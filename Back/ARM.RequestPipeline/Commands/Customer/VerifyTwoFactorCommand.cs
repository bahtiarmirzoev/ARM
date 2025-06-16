using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record VerifyTwoFactorCommand(string C, int O) : IRequest<bool>;