using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record ToggleAvailabilityCommand(string AutoServiceId, bool IsAvailable) : IRequest<bool>; 