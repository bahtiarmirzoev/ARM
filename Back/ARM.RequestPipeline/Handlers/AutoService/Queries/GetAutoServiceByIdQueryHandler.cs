using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Queries;

public class GetAutoServiceByIdQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetAutoServiceByIdQuery, BrandDto>
{
    public async Task<BrandDto> Handle(GetAutoServiceByIdQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByIdAsync(request.Id);
    }
} 