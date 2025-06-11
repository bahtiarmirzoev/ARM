using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;

namespace ARM.Core.Abstractions.Services.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto loginDto);
    Task<string> RegisterAsync(CreateUserDto createUserDto);
    Task<UserDto> ConfirmOtpAsync(string sessionId, string otp);
    Task<AccessInfoDto> RefreshTokenAsync();
    Task<bool> LogoutAsync();
}