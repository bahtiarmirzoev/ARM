using ARM.Core.Dtos.Auth;

namespace ARM.Core.Abstractions.Services.Auth;

public interface IAccountService
{
    Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}