using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Commands;

public class UpdateCapacityCommandHandler(IBrandService brandService) 
    : IRequestHandler<UpdateCapacityCommand, bool>
{
    public async Task<bool> Handle(UpdateCapacityCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateCapacityAsync(request.AutoServiceId, request.MaxConcurrentServices);
    }
} 