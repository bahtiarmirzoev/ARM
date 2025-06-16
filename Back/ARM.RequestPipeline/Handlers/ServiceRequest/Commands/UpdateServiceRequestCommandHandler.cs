using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.ServiceRequest;
using MediatR;

namespace ARM.RequestPipeline.Handlers.ServiceRequest.Commands;

public class UpdateServiceRequestCommandHandler : IRequestHandler<UpdateServiceRequestCommand, ServiceRequestDto>
{
    private readonly IServiceRequestService _serviceRequestService;

    public UpdateServiceRequestCommandHandler(IServiceRequestService serviceRequestService)
    {
        _serviceRequestService = serviceRequestService;
    }

    public async Task<ServiceRequestDto> Handle(UpdateServiceRequestCommand request, CancellationToken cancellationToken)
    {
        return await _serviceRequestService.UpdateAsync(request.Id, request.ServiceRequest);
    }
}