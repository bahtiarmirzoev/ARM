using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record CreateCustomerCommand(CreateCustomerDto Customer) : IRequest<CustomerDto>;