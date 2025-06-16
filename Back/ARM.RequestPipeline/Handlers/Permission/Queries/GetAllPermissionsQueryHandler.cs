using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Permission;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Permission.Queries;

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>
{
    private readonly IPermissionService _permissionService;

    public GetAllPermissionsQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _permissionService.GetAllAsync();
    }
}