using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.WorkingHour;
using MediatR;

namespace ARM.RequestPipeline.Handlers.WorkingHour.Commands;

public class DeleteWorkingHourCommandHandler : IRequestHandler<DeleteWorkingHourCommand, bool>
{
    private readonly IWorkingHourService _workingHourService;

    public DeleteWorkingHourCommandHandler(IWorkingHourService workingHourService)
    {
        _workingHourService = workingHourService;
    }

    public async Task<bool> Handle(DeleteWorkingHourCommand request, CancellationToken cancellationToken)
    {
        return await _workingHourService.DeleteAsync(request.Id);
    }
}