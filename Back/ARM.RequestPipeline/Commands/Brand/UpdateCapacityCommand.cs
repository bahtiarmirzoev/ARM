using MediatR;

namespace ARM.RequestPipeline.Commands.Brand;

public record UpdateCapacityCommand(string Id, int MaxConcurrentServices) : IRequest<bool>;