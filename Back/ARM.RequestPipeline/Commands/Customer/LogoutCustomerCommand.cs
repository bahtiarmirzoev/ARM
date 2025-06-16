using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record LogoutCustomerCommand() : IRequest<bool>;