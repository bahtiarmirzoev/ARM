using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Queries;

public class GetRolePermissionsQueryHandler(IRoleService roleService)
    : IRequestHandler<GetRolePermissionsQuery, IEnumerable<PermissionDto>>
{
    public async Task<IEnumerable<PermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetRolePermissionsAsync(request.RoleId);
    }
}