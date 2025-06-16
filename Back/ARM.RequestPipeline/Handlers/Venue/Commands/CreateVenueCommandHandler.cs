using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Venue;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Venue.Commands;

public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, VenueDto>
{
    private readonly IVenueService _venueService;

    public CreateVenueCommandHandler(IVenueService venueService)
    {
        _venueService = venueService;
    }

    public async Task<VenueDto> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        return await _venueService.CreateAsync(request.Venue);
    }
}