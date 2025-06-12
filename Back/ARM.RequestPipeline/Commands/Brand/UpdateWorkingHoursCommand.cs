using MediatR;

namespace ARM.RequestPipeline.Commands.Brand;

public record UpdateWorkingHoursCommand(string Id, TimeSpan StartTime, TimeSpan EndTime) : IRequest<bool>;