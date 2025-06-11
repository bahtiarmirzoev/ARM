using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record LogoutCommand : IRequest<bool>;