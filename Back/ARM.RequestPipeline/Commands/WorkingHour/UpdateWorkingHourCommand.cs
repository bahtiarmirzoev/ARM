using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.WorkingHour;

public record UpdateWorkingHourCommand(string Id, UpdateWorkingHourDto WorkingHour) : IRequest<WorkingHourDto>;