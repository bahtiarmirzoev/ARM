using System.Security.Claims;
using System.Text.RegularExpressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using static NanoidDotNet.Nanoid;
using static BCrypt.Net.BCrypt;
using static ARM.Core.Constants.ValidationConstants;

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
    public HttpContext httpContext => httpContextAccessor.HttpContext;

    private string GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        switch (loginDto)
        {
            case null:
            case { Email: null or "", Password: null or "" }:
                throw new AppException(ExceptionType.InvalidRequest, "EmailAndPasswordCannotBeEmpty");
        }

        switch (loginDto.Email)
        {
            case var email when !Regex.IsMatch(email, EmailRegex):
                throw new AppException(ExceptionType.InvalidRequest, "InvalidEmailFormat");
        }

        var user = await userService.GetUserByEmailAsync(loginDto.Email)
            ?? throw new AppException(ExceptionType.InvalidRequest, "UserDoesNotExist");

        var userCredentials = await userService.GetUserCredentialsByIdAsync(user.Id);

        switch (Verify(loginDto.Password, userCredentials.Password))
        {
            case false:
                throw new AppException(ExceptionType.InvalidRequest, "InvalidEmailOrPassword");
        }
        
        string requiredRole = loginDto.Tp switch
        {
            "ap" => "Admin",
            "mp" => "Manager",
            "cp" => "Client",
            "sp" => "SuperAdmin",
            _ => throw new AppException(ExceptionType.InvalidRequest, "Unknown")
        };

        if (user.Role.Name != requiredRole)
            throw new AppException(ExceptionType.UnauthorizedAccess, "NoAccess");

        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();
        var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        var deviceInfo = httpContext.Request.Headers["User-Agent"].ToString();

        var createUserActiveSessionDto = new CreateUserActiveSessionDto
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshTokenExpiryTime,
            DeviceInfo = deviceInfo
        };

        await userActiveSessionsService.AddUserActiveSessionAsync(createUserActiveSessionDto);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };

        httpContext.Response.Cookies.Append("atk", accessToken, cookieOptions);
        httpContext.Response.Cookies.Append("rtk", refreshToken, cookieOptions);

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

        var si = Generate(size: 64);

        await otpService.SavePendingUserAsync(si, userDto);
        var otp = await otpService.GenerateAndSaveOtpAsync(si);

        await emailService.SendOtpAsync(userDto.Email, otp);

        return si;
    }

    public async Task<UserDto> ConfirmOtpAsync(string si, string otp)
    {
        if (string.IsNullOrWhiteSpace(si) || string.IsNullOrWhiteSpace(otp))
            throw new AppException(ExceptionType.InvalidRequest, "SessionIdAndOtpRequired");

        var isOtpValid = await otpService.VerifyOtpAsync(si, otp);
        if (!isOtpValid)
            throw new AppException(ExceptionType.InvalidCredentials, "InvalidOrExpiredOtp");

        var pendingUser = await otpService.GetPendingUserAsync(si)
            ?? throw new AppException(ExceptionType.InvalidRequest, "PendingUserNotFound");

        var user = await userService.CreateUserAsync(pendingUser);
        await otpService.ClearOtpAndPendingUserAsync(si);

        return user;
    }

    public async Task<AccessInfoDto> RefreshTokenAsync()
    {
        var userId = GetUserId();
        var accessToken = httpContext.Request.Cookies["atk"];
        var refreshToken = httpContext.Request.Cookies["rtk"];

        switch (accessToken, refreshToken)
        {
            case (null or "", _) or (_, null or ""):
                throw new AppException(ExceptionType.InvalidRefreshToken, "MissingTokens");

            case (_, _) when await blackListedService.IsBlackListedAsync(new BlackListedDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }):
                throw new AppException(ExceptionType.InvalidRefreshToken, "TokenIsBlacklisted");
        }

        var user = await userService.GetUserByIdAsync(userId);
        var userActiveSessions = await userActiveSessionsService.GetUserActiveSessionAsync(userId);

        switch (userActiveSessions.Any())
        {
            case false:
                throw new AppException(ExceptionType.InvalidRequest, "NoActiveSessionsFound");
        }

        var userActiveSession = userActiveSessions.FirstOrDefault(session => session.RefreshToken == refreshToken);

        switch (userActiveSession)
        {
            case null:
                throw new AppException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");
            case { RefreshTokenExpiryTime: var expiryTime } when expiryTime < DateTime.UtcNow:
                throw new AppException(ExceptionType.InvalidRefreshToken, "RefreshTokenExpired");
        }

        var newAccessToken = tokenService.GenerateAccessToken(user!);
        var newRefreshToken = tokenService.GenerateRefreshToken();
        var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        var deviceInfo = httpContext.Request.Headers.UserAgent.ToString();

        var updateUserActiveSessionDto = new UpdateUserActiveSessionDto
        {
            UserId = user.Id,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiryDate = refreshTokenExpiryTime,
            DeviceInfo = deviceInfo
        };
        var newBlackList = new CreateBlackListedDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
        };
        await blackListedService.AddToBlackListAsync(newBlackList);
        await userActiveSessionsService.UpdateUserActiveSessionAsync(userActiveSession.Id, updateUserActiveSessionDto);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };

        httpContext.Response.Cookies.Append("atk", newAccessToken, cookieOptions);
        httpContext.Response.Cookies.Append("rtk", newRefreshToken, cookieOptions);

        return new AccessInfoDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
        };

    }

    public async Task<bool> LogoutAsync()
    {
        var userId = GetUserId();
        var accessToken = httpContext.Request.Cookies["atk"];
        var refreshToken = httpContext.Request.Cookies["rtk"];
        var userActiveSessions =
            await userActiveSessionsService.GetUserActiveSessionAsync(userId);
        var userActiveSession = userActiveSessions
            .FirstOrDefault(session => session.RefreshToken == refreshToken);
        var deviceInfo = httpContext.Request.Headers.UserAgent.ToString();

        switch (accessToken, refreshToken, userActiveSessions.Any(), userActiveSession)
        {
            case (null or "", _, _, _)
                or (_, null or "", _, _):
                throw new AppException(ExceptionType.InvalidRefreshToken, "MissingTokens");

            case (_, _, false, _):
                throw new AppException(ExceptionType.InvalidRefreshToken, "NoActiveSessionsFound");

            case (_, _, _, null):
                throw new AppException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");

            case (_, _, _, { RefreshToken: var token }):
                if (token != refreshToken)
                    throw new AppException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");
                break;
        }

        var newBlackList = new CreateBlackListedDto
        {
            IpAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
            DeviceInfo = deviceInfo,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        await blackListedService.AddToBlackListAsync(newBlackList);
        await userActiveSessionsService.DeleteUserActiveSessionAsync(userActiveSession.Id);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };

        httpContext.Response.Cookies.Delete("atk", cookieOptions);
        httpContext.Response.Cookies.Delete("rtk", cookieOptions);

        return true;
    }
}