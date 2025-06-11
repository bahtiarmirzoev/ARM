using ARM.Core.Abstractions.Services.Auth;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;                                                                         

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await authService.RegisterAsync(request.NewUser);
    }
}