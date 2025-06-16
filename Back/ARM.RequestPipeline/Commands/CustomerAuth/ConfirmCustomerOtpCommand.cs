using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.CustomerAuth;

public record ConfirmCustomerOtpCommand(string S, string O) : IRequest<CustomerDto>;