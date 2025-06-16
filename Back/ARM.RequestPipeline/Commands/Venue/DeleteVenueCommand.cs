using MediatR;

namespace ARM.RequestPipeline.Commands.Venue;

public record DeleteVenueCommand(string Id) : IRequest<bool>;