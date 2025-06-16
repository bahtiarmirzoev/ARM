using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;

namespace ARM.Core.Abstractions.Services.Auth;

public interface ICustomerAuthService
{
    Task<bool> LoginAsync(CustomerLoginDto loginDto);
    Task<string> RegisterAsync(CreateCustomerDto customerDto);
    Task<CustomerDto> ConfirmOtpAsync(string sessionId, int otp);
    Task<bool> VerifyTwoFactorAsync(string customerId, int otpCode);
    Task<AccessInfoDto> RefreshTokenAsync();
    Task<bool> LogoutAsync();
}