using MediatR;

namespace ARM.RequestPipeline.Queries.Role;

public record UserHasRoleQuery(string RoleName) : IRequest<bool>;