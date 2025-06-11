using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.Role;

public record UpdateRoleCommand(string Id, UpdateRoleDto Role) : IRequest<RoleDto>;