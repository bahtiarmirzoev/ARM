using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Commands;

public class AddPermissionToRoleCommandHandler : IRequestHandler<AddPermissionToRoleCommand, RoleDto>
{
    private readonly IRoleService _roleService;

    public AddPermissionToRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<RoleDto> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.AddPermissionToRoleAsync(request.RoleId, request.PermissionNames);
    }
}