using ARM.Core.Dtos.Auth;
using MediatR;

namespace ARM.RequestPipeline.Commands.CustomerAuth;

public record LoginCustomerCommand(CustomerLoginDto Login) : IRequest<bool>;