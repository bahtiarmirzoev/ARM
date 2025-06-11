using ARM.Core.Common;
using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;

namespace ARM.Core.Abstractions.Services.Main;

public interface IUserService
{
    Task<PublicUserDto> GetCurrentUserAsync();
    Task<UserDto> UpdateProfileAsync(UpdateUserDto updateDto);
    Task<string> GetUserPasswordHashAsync(string userId);
    Task UpdateUserPasswordAsync(string userId, string newPassword);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserCredentialsDto?> GetUserCredentialsByIdAsync(string id);
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
    Task<UserDto> ConfirmEmailAsync();
    Task<bool> DeleteUserAsync(string userId);
    Task<PaginatedResponse<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize); 
    Task<UserDto?> GetUserByIdAsync(string id);
}