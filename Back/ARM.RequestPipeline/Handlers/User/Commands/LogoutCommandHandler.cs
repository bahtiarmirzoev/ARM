using ARM.Core.Abstractions.Services.Auth;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;

public class LogoutCommandHandler(IAuthService authService) : IRequestHandler<LogoutCommand, bool>
{
    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return await authService.LogoutAsync();
    }
}