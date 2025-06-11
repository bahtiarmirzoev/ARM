using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;

public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserService _userService = userService;

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.UpdateUserAsync(request.Id, request.User);
    }
}