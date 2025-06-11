using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Queries;

public class GetAutoServicesByRatingQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetAutoServicesByRatingQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetAutoServicesByRatingQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByRatingAsync(request.MinRating);
    }
} 