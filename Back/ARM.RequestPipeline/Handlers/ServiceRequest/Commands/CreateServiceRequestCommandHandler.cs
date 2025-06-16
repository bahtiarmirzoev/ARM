using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.ServiceRequest;
using MediatR;

namespace ARM.RequestPipeline.Handlers.ServiceRequest.Commands;

public class CreateServiceRequestCommandHandler : IRequestHandler<CreateServiceRequestCommand, ServiceRequestDto>
{
    private readonly IServiceRequestService _serviceRequestService;

    public CreateServiceRequestCommandHandler(IServiceRequestService serviceRequestService)
    {
        _serviceRequestService = serviceRequestService;
    }

    public async Task<ServiceRequestDto> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
    {
        return await _serviceRequestService.CreateAsync(request.ServiceRequest);
    }
}