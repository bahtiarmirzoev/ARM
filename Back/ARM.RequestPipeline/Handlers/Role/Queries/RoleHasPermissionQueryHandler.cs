using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Queries.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Queries;

public class RoleHasPermissionQueryHandler(IRoleService roleService) : IRequestHandler<RoleHasPermissionQuery, bool>
{
    private readonly IRoleService _roleService = roleService;

    public async Task<bool> Handle(RoleHasPermissionQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.RoleHasPermissionAsync(request.RoleId, request.PermissionName);
    }
}