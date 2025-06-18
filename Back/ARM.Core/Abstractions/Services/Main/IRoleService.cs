using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;

namespace ARM.Core.Abstractions.Services.Main;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> GetRoleByNameAsync(string name);
    Task<RoleDto> GetCurrentUserRoleAsync();
    Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(string roleId);
    Task<bool> UserHasRoleAsync(string roleName);
    Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleId);
    Task<bool> RoleHasPermissionAsync(string roleId, string permissionName);
    Task<RoleDto> AddPermissionToRoleAsync(string roleId, List<string> permissionNames);
    Task<RoleDto> CreateAsync(CreateRoleDto roleDto);
    Task<RoleDto> UpdateAsync(string id, UpdateRoleDto roleDto);
    Task<bool> DeleteAsync(string id);
}