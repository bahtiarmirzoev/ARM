using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Permission;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Permission.Commands;

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, bool>
{
    private readonly IPermissionService _permissionService;

    public DeletePermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.DeleteAsync(request.Id);
    }
}