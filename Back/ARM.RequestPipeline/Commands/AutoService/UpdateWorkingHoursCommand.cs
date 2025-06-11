using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record UpdateWorkingHoursCommand(string AutoServiceId, TimeSpan StartTime, TimeSpan EndTime) 
    : IRequest<bool>; 