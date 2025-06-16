using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Create;
using ARM.RequestPipeline.Commands.CustomerAuth;
using FluentValidation;
using MediatR;
using static NanoidDotNet.Nanoid;

namespace ARM.RequestPipeline.Handlers.CustomerAuth.Commands;

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