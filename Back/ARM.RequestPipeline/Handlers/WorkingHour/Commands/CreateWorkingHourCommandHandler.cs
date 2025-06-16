using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.WorkingHour;
using MediatR;

namespace ARM.RequestPipeline.Handlers.WorkingHour.Commands;

public class CreateWorkingHourCommandHandler : IRequestHandler<CreateWorkingHourCommand, WorkingHourDto>
{
    private readonly IWorkingHourService _workingHourService;

    public CreateWorkingHourCommandHandler(IWorkingHourService workingHourService)
    {
        _workingHourService = workingHourService;
    }

    public async Task<WorkingHourDto> Handle(CreateWorkingHourCommand request, CancellationToken cancellationToken)
    {
        return await _workingHourService.CreateAsync(request.WorkingHour);
    }
}