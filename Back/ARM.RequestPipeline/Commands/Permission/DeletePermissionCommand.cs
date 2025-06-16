using MediatR;

namespace ARM.RequestPipeline.Commands.Permission;

public record DeletePermissionCommand(string Id) : IRequest<bool>;