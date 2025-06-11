using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Role;

public record GetUsersInRoleQuery(string RoleId) : IRequest<IEnumerable<UserDto>>;