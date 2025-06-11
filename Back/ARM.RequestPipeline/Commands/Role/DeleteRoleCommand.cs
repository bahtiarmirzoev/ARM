using MediatR;

namespace ARM.RequestPipeline.Commands.Role;

public record DeleteRoleCommand(string Id) : IRequest<bool>;