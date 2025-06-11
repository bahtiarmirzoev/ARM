using ARM.Core.Dtos.Auth;
using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record LoginCommand(LoginDto Login) : IRequest<bool>;