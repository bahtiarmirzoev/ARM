using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Queries;

public class GetAllBrandsQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetAllAsync();
    }
} 