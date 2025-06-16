using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;

namespace ARM.Core.Abstractions.Services.Auth;

public interface ICustomerAuthService
{
    Task<bool> LoginAsync(CustomerLoginDto loginDto);
    Task<string> RegisterAsync(CreateCustomerDto customerDto);
    Task<CustomerDto> ConfirmOtpAsync(string sessionId, string otp);
    Task<AccessInfoDto> RefreshTokenAsync();
    Task<bool> LogoutAsync();
}