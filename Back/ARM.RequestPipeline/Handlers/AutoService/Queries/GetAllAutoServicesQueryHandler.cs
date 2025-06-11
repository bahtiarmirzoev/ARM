using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Queries;

public class GetAllAutoServicesQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetAllAutoServicesQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetAllAutoServicesQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetAllAsync();
    }
} 