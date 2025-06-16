using ARM.Core.Dtos.Create;
using MediatR;

namespace ARM.RequestPipeline.Commands.CustomerAuth;

public record RegisterCustomerCommand(CreateCustomerDto NewCustomer) : IRequest<string>;