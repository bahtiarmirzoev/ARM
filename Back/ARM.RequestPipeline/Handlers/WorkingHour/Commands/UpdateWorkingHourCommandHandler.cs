using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.WorkingHour;
using MediatR;

namespace ARM.RequestPipeline.Handlers.WorkingHour.Commands;

public class UpdateWorkingHourCommandHandler : IRequestHandler<UpdateWorkingHourCommand, WorkingHourDto>
{
    private readonly IWorkingHourService _workingHourService;

    public UpdateWorkingHourCommandHandler(IWorkingHourService workingHourService)
    {
        _workingHourService = workingHourService;
    }

    public async Task<WorkingHourDto> Handle(UpdateWorkingHourCommand request, CancellationToken cancellationToken)
    {
        return await _workingHourService.UpdateAsync(request.Id, request.WorkingHour);
    }
}