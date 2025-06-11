using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record UpdateUserProfileCommand(UpdateUserDto User) : IRequest<UserDto>;