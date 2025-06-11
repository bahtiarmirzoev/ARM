using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Queries;

public class GetAutoServicesByPriceRangeQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetAutoServicesByPriceRangeQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetAutoServicesByPriceRangeQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByPriceRangeAsync(request.MinPrice, request.MaxPrice);
    }
} 