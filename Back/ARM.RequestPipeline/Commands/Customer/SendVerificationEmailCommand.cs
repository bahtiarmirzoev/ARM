using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record SendVerificationEmailCommand : IRequest<bool>;