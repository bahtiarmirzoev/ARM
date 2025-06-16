using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Permission;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Permission.Commands;

public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
{
    private readonly IPermissionService _permissionService;

    public UpdatePermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.UpdateAsync(request.Id, request.Permission);
    }
}