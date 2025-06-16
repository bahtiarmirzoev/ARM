using ARM.Core.Dtos.Read;

namespace ARM.Core.Abstractions.Services.Auth;

public interface ITokenService
{
    string GenerateAccessToken(UserDto user);
    string GenerateAccessToken(CustomerDto customer);
    string GenerateRefreshToken();
    string GenerateRandomPassword(int length = 12);
}