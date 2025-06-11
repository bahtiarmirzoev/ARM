using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Role;

public record CreateRoleCommand(CreateRoleDto Role) : IRequest<RoleDto>;