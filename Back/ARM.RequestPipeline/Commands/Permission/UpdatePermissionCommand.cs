using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.Permission;

public record UpdatePermissionCommand(string Id, UpdatePermissionDto Permission) : IRequest<PermissionDto>;