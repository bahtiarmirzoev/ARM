using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Commands;

public class UpdateCapacityCommandHandler(IBrandService brandService)
    : IRequestHandler<UpdateCapacityCommand, bool>
{
    public async Task<bool> Handle(UpdateCapacityCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateCapacityAsync(request.Id, request.MaxConcurrentServices);
    }
}