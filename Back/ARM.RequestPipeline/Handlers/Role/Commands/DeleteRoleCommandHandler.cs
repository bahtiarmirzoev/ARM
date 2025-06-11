using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Role;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Role.Commands;

public class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.DeleteAsync(request.Id);
    }
}