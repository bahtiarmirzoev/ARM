using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Venue;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Venue.Commands;

public class UpdateVenueCommandHandler : IRequestHandler<UpdateVenueCommand, VenueDto>
{
    private readonly IVenueService _venueService;

    public UpdateVenueCommandHandler(IVenueService venueService)
    {
        _venueService = venueService;
    }

    public async Task<VenueDto> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
    {
        return await _venueService.UpdateAsync(request.Id, request.Venue);
    }
}