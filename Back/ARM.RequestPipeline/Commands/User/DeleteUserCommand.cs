using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record DeleteUserCommand(string UserId) : IRequest<bool>;