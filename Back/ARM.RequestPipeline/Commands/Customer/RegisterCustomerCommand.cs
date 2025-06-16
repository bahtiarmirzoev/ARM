using ARM.Core.Dtos.Create;
using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record RegisterCustomerCommand(CreateCustomerDto NewCustomer) : IRequest<string>;