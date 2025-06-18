using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, LoginResponseDto>
{
    private readonly ICustomerAuthService _customerAuthService;

    public LoginCustomerCommandHandler(ICustomerAuthService customerAuthService)
    {
        _customerAuthService = customerAuthService;
    }

    public async Task<LoginResponseDto> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerAuthService.LoginAsync(request.Login);
    }
}