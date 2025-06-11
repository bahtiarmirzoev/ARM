using MediatR;

namespace ARM.RequestPipeline.Queries.Role;

public record RoleHasPermissionQuery(string RoleId, string PermissionName) : IRequest<bool>;