using ARM.Core.Abstractions.Services.Auth;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, bool>
{
    public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request.Login);
    }
}