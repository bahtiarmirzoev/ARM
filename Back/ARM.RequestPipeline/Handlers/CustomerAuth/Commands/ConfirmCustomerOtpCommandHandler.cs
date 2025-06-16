using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.CustomerAuth;
using MediatR;

namespace ARM.RequestPipeline.Handlers.CustomerAuth.Commands;

public class ConfirmCustomerOtpCommandHandler : IRequestHandler<ConfirmCustomerOtpCommand, CustomerDto>
{
    private readonly ICustomerAuthService _customerAuthService;

    public ConfirmCustomerOtpCommandHandler(ICustomerAuthService customerAuthService)
    {
        _customerAuthService = customerAuthService;
    }

    public async Task<CustomerDto> Handle(ConfirmCustomerOtpCommand request, CancellationToken cancellationToken)
    {
        return await _customerAuthService.ConfirmOtpAsync(request.S, request.O);
    }
}