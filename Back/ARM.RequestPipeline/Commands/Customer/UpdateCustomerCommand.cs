using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record UpdateCustomerCommand(string Id, UpdateCustomerDto Customer) : IRequest<CustomerDto>;