using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface IPermissionService
{
    Task<PermissionDto> CreateAsync(CreatePermissionDto dto);
    Task<PermissionDto> UpdateAsync(string id, UpdatePermissionDto dto);
    Task<bool> DeleteAsync(string id);
    Task<PermissionDto> GetByIdAsync(string id);
    Task<IEnumerable<PermissionDto>> GetAllAsync();
    Task<PermissionDto> GetByNameAsync(string name);
    Task<IEnumerable<PermissionDto>> GetByRoleIdAsync(string roleId);
} 