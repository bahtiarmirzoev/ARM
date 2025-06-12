using MediatR;

namespace ARM.RequestPipeline.Commands.Brand;

public record ToggleAvailabilityCommand(string Id, bool IsAvailable) : IRequest<bool>;