using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Queries;

public class GetAutoServicesByRatingQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetBrandsByRatingQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetBrandsByRatingQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByRatingAsync(request.MinRating);
    }
} 