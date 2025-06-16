using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Venue;

public record CreateVenueCommand(CreateVenueDto Venue) : IRequest<VenueDto>;