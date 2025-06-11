using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Queries;

public class GetCurrentUserRoleQueryHandler(IRoleService roleService)
    : IRequestHandler<GetCurrentUserRoleQuery, RoleDto>
{
    public async Task<RoleDto> Handle(GetCurrentUserRoleQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetCurrentUserRoleAsync();
    }
}