using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Queries;

public class GetBrandByIdQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetBrandByIdQuery, BrandDto>
{
    public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByIdAsync(request.Id);
    }
} 