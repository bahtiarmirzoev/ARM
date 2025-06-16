using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Permission;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Permission.Commands;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
{
    private readonly IPermissionService _permissionService;

    public CreatePermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.CreateAsync(request.Permission);
    }
}