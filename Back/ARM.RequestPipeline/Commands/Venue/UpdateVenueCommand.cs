using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.Venue;

public record UpdateVenueCommand(string Id, UpdateVenueDto Venue) : IRequest<VenueDto>;