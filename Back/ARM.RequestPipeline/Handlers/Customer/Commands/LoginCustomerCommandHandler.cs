using ARM.Core.Abstractions.Services.Auth;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, bool>
{
    private readonly ICustomerAuthService _customerAuthService;

    public LoginCustomerCommandHandler(ICustomerAuthService customerAuthService)
    {
        _customerAuthService = customerAuthService;
    }

    public async Task<bool> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerAuthService.LoginAsync(request.Login);
    }
}