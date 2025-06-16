using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.ServiceRequest;

public record CreateServiceRequestCommand(CreateServiceRequestDto ServiceRequest) : IRequest<ServiceRequestDto>;