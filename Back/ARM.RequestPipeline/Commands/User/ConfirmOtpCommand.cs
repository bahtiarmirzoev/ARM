using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.User;

public record ConfirmOtpCommand(string Si, string Otp) : IRequest<UserDto>;