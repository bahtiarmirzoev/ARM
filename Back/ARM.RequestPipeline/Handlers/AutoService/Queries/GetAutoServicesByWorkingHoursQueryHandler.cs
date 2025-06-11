using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Queries;

public class GetAutoServicesByWorkingHoursQueryHandler(IBrandService brandService) 
    : IRequestHandler<GetAutoServicesByWorkingHoursQuery, IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetAutoServicesByWorkingHoursQuery request, CancellationToken cancellationToken)
    {
        return await brandService.GetByWorkingHoursAsync(request.StartTime, request.EndTime);
    }
} 