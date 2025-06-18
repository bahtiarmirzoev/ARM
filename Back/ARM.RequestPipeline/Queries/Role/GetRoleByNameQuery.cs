using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Role;

public record GetRoleByNameQuery(string Name) : IRequest<RoleDto>;