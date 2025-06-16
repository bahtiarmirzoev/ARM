using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record DeleteCustomerCommand(string Id) : IRequest<bool>;