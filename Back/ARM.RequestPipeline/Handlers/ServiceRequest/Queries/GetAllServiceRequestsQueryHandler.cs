using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.ServiceRequest;
using MediatR;

namespace ARM.RequestPipeline.Handlers.ServiceRequest.Queries;

public class GetAllServiceRequestsQueryHandler : IRequestHandler<GetAllServiceRequestsQuery, IEnumerable<ServiceRequestDto>>
{
    private readonly IServiceRequestService _serviceRequestService;

    public GetAllServiceRequestsQueryHandler(IServiceRequestService serviceRequestService)
    {
        _serviceRequestService = serviceRequestService;
    }

    public async Task<IEnumerable<ServiceRequestDto>> Handle(GetAllServiceRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _serviceRequestService.GetAllAsync();
    }
}