using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Venue;

public record GetAllVenuesQuery() : IRequest<IEnumerable<VenueDto>>;