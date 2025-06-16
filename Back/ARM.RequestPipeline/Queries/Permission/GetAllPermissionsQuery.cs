using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Permission;

public record GetAllPermissionsQuery() : IRequest<IEnumerable<PermissionDto>>;