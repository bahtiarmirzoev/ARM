using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record CreateUserCommand(CreateUserDto User) : IRequest<UserDto>;