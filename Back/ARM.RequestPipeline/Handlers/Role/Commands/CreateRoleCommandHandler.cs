using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Commands;

public class CreateRoleCommandHandler(IRoleService roleService) : IRequestHandler<CreateRoleCommand, RoleDto>
{
    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.CreateAsync(request.Role);
    }
}