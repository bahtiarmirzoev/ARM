using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Venue;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Venue.Queries;

public class GetAllVenuesQueryHandler : IRequestHandler<GetAllVenuesQuery, IEnumerable<VenueDto>>
{
    private readonly IVenueService _venueService;

    public GetAllVenuesQueryHandler(IVenueService venueService)
    {
        _venueService = venueService;
    }

    public async Task<IEnumerable<VenueDto>> Handle(GetAllVenuesQuery request, CancellationToken cancellationToken)
    {
        return await _venueService.GetAllAsync();
    }
}