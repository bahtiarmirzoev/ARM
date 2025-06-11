using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Commands;

public class ToggleAvailabilityCommandHandler(IBrandService brandService) 
    : IRequestHandler<ToggleAvailabilityCommand, bool>
{
    public async Task<bool> Handle(ToggleAvailabilityCommand request, CancellationToken cancellationToken)
    {
        return await brandService.ToggleAvailabilityAsync(request.AutoServiceId, request.IsAvailable);
    }
} 