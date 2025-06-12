using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Queries;

public class GetNearbyAutoServicesQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetNearbyBrandsQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetNearbyBrandsQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetNearbyServicesAsync(request.Latitude, request.Longitude, request.RadiusKm);
    }
} 