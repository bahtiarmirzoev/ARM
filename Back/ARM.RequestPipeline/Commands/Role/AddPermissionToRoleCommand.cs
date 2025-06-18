using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Role;

public record AddPermissionToRoleCommand(string RoleId, List<string> PermissionNames)
    : IRequest<RoleDto>;