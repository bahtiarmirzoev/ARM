using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record UpdateUserCommand(string Id, UpdateUserDto User) : IRequest<UserDto>;