using System.Security.Claims;
using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record SendVerificationEmailCommand : IRequest<bool>;