using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Customer;

public record ConfirmCustomerOtpCommand(string S, int O) : IRequest<CustomerDto>;