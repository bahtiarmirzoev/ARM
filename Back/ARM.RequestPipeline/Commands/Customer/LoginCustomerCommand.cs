using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record LoginCustomerCommand(CustomerLoginDto Login) : IRequest<LoginResponseDto>;