using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Queries;

public class GetAutoServicesByCityQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetAutoServicesByCityQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetAutoServicesByCityQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByCityAsync(request.City);
    }
} 