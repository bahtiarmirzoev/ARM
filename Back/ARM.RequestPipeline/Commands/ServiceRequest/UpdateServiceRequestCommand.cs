using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.ServiceRequest;

public record UpdateServiceRequestCommand(string Id, UpdateServiceRequestDto ServiceRequest) : IRequest<ServiceRequestDto>;