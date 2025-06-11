using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record UpdateCapacityCommand(string AutoServiceId, int MaxConcurrentServices) : IRequest<bool>; 