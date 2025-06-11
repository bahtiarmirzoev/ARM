using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Queries;

public class GetUsersInRoleQueryHandler(IRoleService roleService)
    : IRequestHandler<GetUsersInRoleQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUsersInRoleQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetUsersInRoleAsync(request.RoleId);
    }
}