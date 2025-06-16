using ARM.Core.Abstractions.Services.Auth;
using ARM.RequestPipeline.Commands.CustomerAuth;
using MediatR;

namespace ARM.RequestPipeline.Handlers.CustomerAuth.Commands;

public class LogoutCustomerCommandHandler : IRequestHandler<LogoutCustomerCommand, bool>
{
    private readonly ICustomerAuthService _customerAuthService;

    public LogoutCustomerCommandHandler(ICustomerAuthService customerAuthService)
    {
        _customerAuthService = customerAuthService;
    }

    public async Task<bool> Handle(LogoutCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerAuthService.LogoutAsync();
    }
}