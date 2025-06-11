using System.Security.Claims;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using static BCrypt.Net.BCrypt;

namespace ARM.Application.Services.Auth;

public class AccountService(
    IUserService userService,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor)
    : IAccountService
{
    private string GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    
    public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var userId = GetUserId();
        var currentPassword = await userService.GetUserPasswordHashAsync(userId);

        if (!Verify(changePasswordDto.CurrentPassword, currentPassword))
            throw new AppException(ExceptionType.InvalidRequest, "OldPasswordIncorrect");
        
        var newPassword = HashPassword(changePasswordDto.NewPassword);
        await userService.UpdateUserPasswordAsync(userId, newPassword); 

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await userService.GetUserByEmailAsync(resetPasswordDto.Email);

        var newPassword = tokenService.GenerateRandomPassword();
        var hashedPassword = HashPassword(newPassword);
        
        //var updateUserDto = new UpdateUserDto { Password = hashedPassword };
       
        //await _userService.UpdateUserAsync(user.Id, updateUserDto);

        return true;
    }
    
}