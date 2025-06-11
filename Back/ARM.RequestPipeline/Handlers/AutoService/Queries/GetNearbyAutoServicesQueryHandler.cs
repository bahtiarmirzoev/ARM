using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Queries;

public class GetNearbyAutoServicesQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetNearbyAutoServicesQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetNearbyAutoServicesQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetNearbyServicesAsync(request.Latitude, request.Longitude, request.RadiusKm);
    }
} 