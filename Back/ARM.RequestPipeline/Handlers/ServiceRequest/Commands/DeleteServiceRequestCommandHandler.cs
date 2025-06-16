using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.ServiceRequest;
using MediatR;

namespace ARM.RequestPipeline.Handlers.ServiceRequest.Commands;

public class DeleteServiceRequestCommandHandler : IRequestHandler<DeleteServiceRequestCommand, bool>
{
    private readonly IServiceRequestService _serviceRequestService;

    public DeleteServiceRequestCommandHandler(IServiceRequestService serviceRequestService)
    {
        _serviceRequestService = serviceRequestService;
    }

    public async Task<bool> Handle(DeleteServiceRequestCommand request, CancellationToken cancellationToken)
    {
        return await _serviceRequestService.DeleteAsync(request.Id);
    }
}