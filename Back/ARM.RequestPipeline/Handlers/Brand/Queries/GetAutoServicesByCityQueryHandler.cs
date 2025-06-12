using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Queries;

public class GetAutoServicesByCityQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetBrandsByCityQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetBrandsByCityQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByCityAsync(request.City);
    }
} 