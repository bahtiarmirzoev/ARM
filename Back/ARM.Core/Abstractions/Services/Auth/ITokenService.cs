using ARM.Core.Dtos.Read;

namespace ARM.Core.Abstractions.Services.Auth;

public interface ITokenService
{
    string GenerateAccessToken(UserDto user);
    string GenerateRefreshToken();
    string GenerateRandomPassword(int length = 12);
}