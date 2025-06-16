using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class VerifyTwoFactorCommandHandler : IRequestHandler<VerifyTwoFactorCommand, bool>
{
    private readonly ICustomerAuthService _customerAuthService;

    public VerifyTwoFactorCommandHandler(ICustomerAuthService customerAuthService)
    {
        _customerAuthService = customerAuthService;
    }
    public async Task<bool> Handle(VerifyTwoFactorCommand request, CancellationToken cancellationToken)
    {
        return await _customerAuthService.VerifyTwoFactorAsync(request.C, request.O);
    }
}