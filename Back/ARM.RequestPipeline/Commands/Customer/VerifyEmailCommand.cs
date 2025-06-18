using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record VerifyEmailCommand(string Token) : IRequest<bool>;