using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.WorkingHour;
using MediatR;

namespace ARM.RequestPipeline.Handlers.WorkingHour.Queries;

public class GetAllWorkingHoursQueryHandler : IRequestHandler<GetAllWorkingHoursQuery, IEnumerable<WorkingHourDto>>
{
    private readonly IWorkingHourService _workingHourService;

    public GetAllWorkingHoursQueryHandler(IWorkingHourService workingHourService)
    {
        _workingHourService = workingHourService;
    }

    public async Task<IEnumerable<WorkingHourDto>> Handle(GetAllWorkingHoursQuery request, CancellationToken cancellationToken)
    {
        return await _workingHourService.GetAllAsync();
    }
}