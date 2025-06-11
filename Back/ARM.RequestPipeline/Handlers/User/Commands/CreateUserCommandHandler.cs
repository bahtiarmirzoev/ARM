using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await userService.CreateUserAsync(request.User);
    }
}