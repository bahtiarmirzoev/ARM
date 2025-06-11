using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;

public class DeleteUserCommandHandler(IUserService userService) : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserService _userService = userService;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.DeleteUserAsync(request.UserId);
    }
}