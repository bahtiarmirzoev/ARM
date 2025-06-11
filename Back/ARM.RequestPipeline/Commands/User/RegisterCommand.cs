using ARM.Core.Dtos.Create;
using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record RegisterCommand(CreateUserDto NewUser) : IRequest<string>;