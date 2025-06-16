using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Permission;

public record CreatePermissionCommand(CreatePermissionDto Permission) : IRequest<PermissionDto>;