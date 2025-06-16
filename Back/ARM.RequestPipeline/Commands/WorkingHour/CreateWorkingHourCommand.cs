using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.WorkingHour;

public record CreateWorkingHourCommand(CreateWorkingHourDto WorkingHour) : IRequest<WorkingHourDto>;