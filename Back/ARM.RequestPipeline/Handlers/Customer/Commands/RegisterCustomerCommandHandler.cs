using ARM.Core.Abstractions.Services.Auth;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, string>
{
    private readonly ICustomerAuthService _customerAuthService;

    public RegisterCustomerCommandHandler(ICustomerAuthService customerAuthService)
    {
        _customerAuthService = customerAuthService;
    }

    public async Task<string> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerAuthService.RegisterAsync(request.NewCustomer);
    }
}