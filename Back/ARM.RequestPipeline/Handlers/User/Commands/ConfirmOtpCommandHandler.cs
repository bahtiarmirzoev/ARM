using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;

public class ConfirmOtpCommandHandler(IAuthService authService) : IRequestHandler<ConfirmOtpCommand, UserDto>
{
    public async Task<UserDto> Handle(ConfirmOtpCommand request, CancellationToken cancellationToken)
    {
        return await authService.ConfirmOtpAsync(request.Si, request.Otp);
    }
}