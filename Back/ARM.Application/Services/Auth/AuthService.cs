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

public class AuthService(
    IUserService userService,
    IEmailService emailService,
    IOtpService otpService,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<CreateUserDto> createUserValidator,
    IUserRepository userRepository,
    IBlackListedService blackListedService,
    IUserActiveSessionsService userActiveSessionsService)
    : IAuthService
{
    private const int RefreshTokenLifespanDays = 7;
    private HttpContext httpContext => httpContextAccessor.HttpContext;
    private HttpRequest httpRequest => httpContext.Request;

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        if (loginDto is null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            throw new AppException(ExceptionType.InvalidRequest, "EmailAndPasswordCannotBeEmpty");

        if (!Regex.IsMatch(loginDto.Email, EmailRegex))
            throw new AppException(ExceptionType.InvalidRequest, "InvalidEmailFormat");
        
        string requiredRole = loginDto.Tp switch
        {
            "ap" => "Admin",
            "mp" => "Manager",
            "sp" => "SuperAdmin", 
            _ => throw new AppException(ExceptionType.InvalidRequest, "Unknown")
        };

        var user = await userService.GetUserByEmailAsync(loginDto.Email)
                   ?? throw new AppException(ExceptionType.InvalidRequest, "UserDoesNotExist");

        var credentials = await userService.GetUserCredentialsByIdAsync(user.Id);

        if (!Verify(loginDto.Password, credentials.Password))
            throw new AppException(ExceptionType.InvalidRequest, "InvalidEmailOrPassword");
        
        if (user.Role.Name != requiredRole)
            throw new AppException(ExceptionType.UnauthorizedAccess, "NoAccess");

        string accessToken;
        switch (requiredRole)
        {
            case "Admin":
            case "Manager":
                if (string.IsNullOrWhiteSpace(user.BrandId))
                    throw new AppException(ExceptionType.InvalidRequest, "UserHasNoBrandAssigned");
                accessToken = tokenService.GenerateAccessToken(user);
                break;
            case "SuperAdmin":
                accessToken = tokenService.GenerateAccessToken(user);
                break;
            default:
                throw new AppException(ExceptionType.InvalidRequest, "UnsupportedRole");
        }

        var refreshToken = tokenService.GenerateRefreshToken();

        var refreshExpiry = DateTime.UtcNow.AddDays(RefreshTokenLifespanDays);

        await userActiveSessionsService.AddUserActiveSessionAsync(new CreateUserActiveSessionDto
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshExpiry,
            DeviceInfo = httpRequest.GetDeviceInfo()
        });

        httpContext.SetAuthCookies(accessToken, refreshToken);

        return true;
    }

    public async Task<string> RegisterAsync(CreateUserDto userDto)
    {
        await createUserValidator.ValidateAndThrowAsync(userDto);

        var existingUser = (await userRepository.FindAsync(u => u.Email == userDto.Email
            || u.PhoneNumber == userDto.PhoneNumber))
            .FirstOrDefault();

        if (existingUser is not null)
            throw new AppException(ExceptionType.Conflict, "UserAlreadyExists");

        var sessionId = Generate(size: 64);

        await otpService.SavePendingUserAsync(sessionId, userDto);
        var otp = await otpService.GenerateAndSaveOtpAsync(sessionId);

        await emailService.SendOtpAsync(userDto.Email, otp);

        return sessionId;
    }

    public async Task<UserDto> ConfirmOtpAsync(string sessionId, string otp)
    {
        if (string.IsNullOrWhiteSpace(sessionId) || string.IsNullOrWhiteSpace(otp))
            throw new AppException(ExceptionType.InvalidRequest, "SessionIdAndOtpRequired");

        var isOtpValid = await otpService.VerifyOtpAsync(sessionId, otp);
        if (!isOtpValid)
            throw new AppException(ExceptionType.InvalidCredentials, "InvalidOrExpiredOtp");

        var pendingUser = await otpService.GetPendingUserAsync(sessionId)
            ?? throw new AppException(ExceptionType.InvalidRequest, "PendingUserNotFound");

        var user = await userService.CreateUserAsync(pendingUser);
        await otpService.ClearOtpAndPendingUserAsync(sessionId);

        return user;
    }

    public async Task<AccessInfoDto> RefreshTokenAsync()
    {
        var userId = httpContext.GetUserId();

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

        var user = await userService.GetUserByIdAsync(userId);
        var sessions = await userActiveSessionsService.GetUserActiveSessionAsync(userId);
        if (!sessions.Any())
            throw new AppException(ExceptionType.InvalidRequest, "NoActiveSessionsFound");

        var session = sessions.FirstOrDefault(x => x.RefreshToken == rtk)
            ?? throw new AppException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");

        if (session.RefreshTokenExpiryTime < DateTime.UtcNow)
            throw new AppException(ExceptionType.InvalidRefreshToken, "RefreshTokenExpired");

        var newAtk = tokenService.GenerateAccessToken(user);
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
            UserId = user.Id,
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
        var userId = httpContext.GetUserId();

        var atk = httpContext.GetAccessToken();
        var rtk = httpContext.GetRefreshToken();

        if (string.IsNullOrWhiteSpace(atk) || string.IsNullOrWhiteSpace(rtk))
            throw new AppException(ExceptionType.InvalidRefreshToken, "MissingTokens");

        var sessions = await userActiveSessionsService.GetUserActiveSessionAsync(userId);
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
