using ARM.Core.Dtos.Auth;
using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record LoginCustomerCommand(CustomerLoginDto Login) : IRequest<bool>;