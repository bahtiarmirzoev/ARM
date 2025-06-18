using System.Text.RegularExpressions;
using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using static ARM.Core.Constants.ValidationConstants;
using static BCrypt.Net.BCrypt;
using static NanoidDotNet.Nanoid;

namespace ARM.Application.Services.Auth;

public class CustomerAuthService(
    ICustomerService customerService,
    IEmailService emailService,
    IOtpService otpService,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<CreateCustomerDto> createCustomerValidator,
    ICustomerRepository customerRepository,
    IBlackListedService blackListedService,
    IUserActiveSessionsService userActiveSessionsService)
    : ICustomerAuthService
{
    private const int RefreshTokenLifespanDays = 7;
    private HttpContext httpContext => httpContextAccessor.HttpContext;
    private HttpRequest httpRequest => httpContext.Request;
    public async Task<LoginResponseDto> LoginAsync(CustomerLoginDto loginDto)
    {
        if (loginDto is null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            throw new AppException(ExceptionType.InvalidRequest, "EmailAndPasswordCannotBeEmpty");

        if (!Regex.IsMatch(loginDto.Email, EmailRegex))
            throw new AppException(ExceptionType.InvalidRequest, "InvalidEmailFormat");

        var customer = await customerService.GetCustomerByEmailAsync(loginDto.Email);

        var credentials = await customerService.GetCustomerCredentialsByIdAsync(customer.Id);

        if (!Verify(loginDto.Password, credentials.Password))
            throw new AppException(ExceptionType.InvalidRequest, "InvalidEmailOrPassword");
        
        if (customer.TwoFaEnabled)
        {
            var otpCode = await otpService.GenerateAndSaveOtpAsync(customer.Id);
            await emailService.SendOtpAsync(customer.Email, otpCode);
            return new LoginResponseDto { S2Fa = true };
        }
        var accessToken = tokenService.GenerateAccessToken(customer);
        var refreshToken = tokenService.GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(RefreshTokenLifespanDays);

        await userActiveSessionsService.AddUserActiveSessionAsync(new CreateUserActiveSessionDto
        {
            UserId = customer.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshExpiry,
            DeviceInfo = httpRequest.GetDeviceInfo()
        });

        httpContext.SetAuthCookies(accessToken, refreshToken);

        return new LoginResponseDto { S2Fa = false };
    }

    public async Task<string> RegisterAsync(CreateCustomerDto customerDto)
    {
        await createCustomerValidator.ValidateAndThrowAsync(customerDto);

        var existingCustomer = (await customerRepository.FindAsync(c => c.Email == customerDto.Email
            || c.PhoneNumber == customerDto.PhoneNumber))
            .FirstOrDefault();

        if (existingCustomer is not null)
            throw new AppException(ExceptionType.Conflict, "CustomerAlreadyExists");

        var sessionId = Generate(size: 64);

        await otpService.SavePendingCustomerAsync(sessionId, customerDto);
        var otp = await otpService.GenerateAndSaveOtpAsync(sessionId);

        await emailService.SendOtpAsync(customerDto.Email, otp);

        return sessionId;
    }

    public async Task<CustomerDto> ConfirmOtpAsync(string sessionId, int otp)
    {
        if (string.IsNullOrWhiteSpace(sessionId) || int.IsNegative(otp))
            throw new AppException(ExceptionType.InvalidRequest, "SessionIdAndOtpRequired");

        var isOtpValid = await otpService.VerifyOtpAsync(sessionId, otp);
        if (!isOtpValid)
            throw new AppException(ExceptionType.InvalidCredentials, "InvalidOrExpiredOtp");

        var pendingCustomer = await otpService.GetPendingCustomerAsync(sessionId)
            ?? throw new AppException(ExceptionType.InvalidRequest, "PendingCustomerNotFound");

        var customer = await customerService.CreateAsync(pendingCustomer);
        await otpService.ClearOtpAndPendingCustomerAsync(sessionId);

        return customer;
    }
    
    public async Task<bool> VerifyTwoFactorAsync(string customerId, int otpCode)
    {
        if (int.IsNegative(otpCode))
            throw new AppException(ExceptionType.InvalidRequest, "OtpCodeRequired");

        var isValid = await otpService.VerifyOtpAsync(customerId, otpCode);
        if (!isValid)
            throw new AppException(ExceptionType.InvalidCredentials, "InvalidOrExpiredOtp");

        var customer = await customerService.GetByIdAsync(customerId);
        var accessToken = tokenService.GenerateAccessToken(customer);
        var refreshToken = tokenService.GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(RefreshTokenLifespanDays);

        await userActiveSessionsService.AddUserActiveSessionAsync(new CreateUserActiveSessionDto
        {
            UserId = customer.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshExpiry,
            DeviceInfo = httpRequest.GetDeviceInfo()
        });

        httpContext.SetAuthCookies(accessToken, refreshToken);
        
        await emailService.SendMessageAsync(customer.Email, customer.Name, customer.Surname);
        await otpService.ClearOtpAndPendingCustomerAsync(customerId);

        return true;
    }


    public async Task<AccessInfoDto> RefreshTokenAsync()
    {
        var customerId = httpContext.GetCustomerId();

        var atk = httpContext.GetAccessToken();
        var rtk = httpContext.GetRefreshToken();

        if (string.IsNullOrWhiteSpace(atk) || string.IsNullOrWhiteSpace(rtk))
            throw new AppException(ExceptionType.InvalidRefreshToken, "MissingTokens");

        var isBlackListed = await blackListedService.IsBlackListedAsync(new BlackListedDto
        {
            AccessToken = atk,
            RefreshToken = rtk
        });

        if (isBlackListed)
            throw new AppException(ExceptionType.InvalidRefreshToken, "TokenIsBlacklisted");

        var customer = await customerService.GetByIdAsync(customerId);
        var sessions = await userActiveSessionsService.GetUserActiveSessionAsync(customerId);
        if (!sessions.Any())
            throw new AppException(ExceptionType.InvalidRequest, "NoActiveSessionsFound");

        var session = sessions.FirstOrDefault(x => x.RefreshToken == rtk)
            ?? throw new AppException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");

        if (session.RefreshTokenExpiryTime < DateTime.UtcNow)
            throw new AppException(ExceptionType.InvalidRefreshToken, "RefreshTokenExpired");

        var newAtk = tokenService.GenerateAccessToken(customer);
        var newRtk = tokenService.GenerateRefreshToken();
        var newExpiry = DateTime.UtcNow.AddDays(RefreshTokenLifespanDays);

        await blackListedService.AddToBlackListAsync(new CreateBlackListedDto
        {
            AccessToken = atk,
            RefreshToken = rtk,
            IpAddress = httpContext.GetIpAddress()
        });

        await userActiveSessionsService.UpdateUserActiveSessionAsync(session.Id, new UpdateUserActiveSessionDto
        {
            UserId = customer.Id,
            AccessToken = newAtk,
            RefreshToken = newRtk,
            RefreshTokenExpiryDate = newExpiry,
            DeviceInfo = httpRequest.GetDeviceInfo()
        });

        httpContext.SetAuthCookies(newAtk, newRtk);

        return new AccessInfoDto
        {
            AccessToken = newAtk,
            RefreshToken = newRtk
        };
    }

    public async Task<bool> LogoutAsync()
    {
        var customerId = httpContext.GetCustomerId();

        var atk = httpContext.GetAccessToken();
        var rtk = httpContext.GetRefreshToken();

        if (string.IsNullOrWhiteSpace(atk) || string.IsNullOrWhiteSpace(rtk))
            throw new AppException(ExceptionType.InvalidRefreshToken, "MissingTokens");

        var sessions = await userActiveSessionsService.GetUserActiveSessionAsync(customerId);
        if (!sessions.Any())
            throw new AppException(ExceptionType.InvalidRefreshToken, "NoActiveSessionsFound");

        var session = sessions.FirstOrDefault(x => x.RefreshToken == rtk)
                      ?? throw new AppException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");

        await blackListedService.AddToBlackListAsync(new CreateBlackListedDto
        {
            IpAddress = httpContext.GetIpAddress(),
            DeviceInfo = httpRequest.GetDeviceInfo(),
            AccessToken = atk,
            RefreshToken = rtk
        });

        await userActiveSessionsService.DeleteUserActiveSessionAsync(session.Id);

        httpContext.DeleteAuthCookies();

        return true;
    }
}