using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Queries.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Queries;

public class UserHasRoleQueryHandler(IRoleService roleService) : IRequestHandler<UserHasRoleQuery, bool>
{
    public async Task<bool> Handle(UserHasRoleQuery request, CancellationToken cancellationToken)
    {
        return await roleService.UserHasRoleAsync(request.RoleName);
    }
}