using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Commands;

public class ToggleAvailabilityCommandHandler(IBrandService brandService)
    : IRequestHandler<ToggleAvailabilityCommand, bool>
{
    public async Task<bool> Handle(ToggleAvailabilityCommand request, CancellationToken cancellationToken)
    {
        return await brandService.ToggleAvailabilityAsync(request.Id, request.IsAvailable);
    }
}