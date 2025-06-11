using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Commands;

public class UpdateRoleCommandHandler(IRoleService roleService) : IRequestHandler<UpdateRoleCommand, RoleDto>
{
    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.UpdateAsync(request.Id, request.Role);
    }
}