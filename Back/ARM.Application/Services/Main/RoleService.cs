using System.Security.Claims;
using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ARM.Application.Services.Main;

public class RoleService(
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    IPermissionRepository permissionRepository,
    IValidator<CreateRoleDto> createRoleValidator,
    IValidator<UpdateRoleDto> updateRoleValidator,
    IUnitOfWork unitOfWork)
    : IRoleService
{
    private string GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await roleRepository.GetAllAsync();
        return mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<RoleDto?> GetRoleByNameAsync(string name)
    {
        var role = (await roleRepository.FindAsync(r => r.Name == name))
            .FirstOrDefault()
            .EnsureFound("RoleNotFound");

        return mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> GetCurrentUserRoleAsync()
    {
        var userId = GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        return mapper.Map<RoleDto>(user.Role);
    }

    public async Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(string roleId)
    {
        var permissions = await permissionRepository
                              .FindAsync(p => p.Roles.Any(r => r.Id == roleId))
                          ?? throw new AppException(ExceptionType.NotFound, "RoleNotFound");

        return mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<bool> UserHasRoleAsync(string roleName)
    {
        var userId = GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        return user.Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase);
    }

    public async Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleId)
    {
        var users = await userRepository.FindAsync(u => u.RoleId == roleId);

        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<bool> RoleHasPermissionAsync(string roleId, string permissionName)
    {
        var permissions = await permissionRepository
            .FindAsync(p => p.Roles.Any(r => r.Id == roleId) && p.Name == permissionName);

        return permissions.Any();
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto roleDto)
    {
        await createRoleValidator.ValidateAndThrowAsync(roleDto);

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            var role = mapper.Map<RoleEntity>(roleDto);
            await roleRepository.AddAsync(role);

            return mapper.Map<RoleDto>(role);
        });
    }

    public async Task<RoleDto> UpdateAsync(string id, UpdateRoleDto roleDto)
    {
        var existingRole = await roleRepository.GetByIdAsync(id);

        await updateRoleValidator.ValidateAndThrowAsync(roleDto);

        mapper.Map(roleDto, existingRole);
        await roleRepository.UpdateAsync(new[] { existingRole });
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<RoleDto>(existingRole);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await roleRepository.DeleteAsync(id);

            return true;
        });
    }
}