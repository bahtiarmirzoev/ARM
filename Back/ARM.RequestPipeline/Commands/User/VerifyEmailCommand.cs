using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record VerifyEmailCommand(string Token) : IRequest<bool>;