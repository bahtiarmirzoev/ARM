using MediatR;

namespace ARM.RequestPipeline.Commands.WorkingHour;

public record DeleteWorkingHourCommand(string Id) : IRequest<bool>;