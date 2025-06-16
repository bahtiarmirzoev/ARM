using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Venue;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Venue.Commands;

public class DeleteVenueCommandHandler : IRequestHandler<DeleteVenueCommand, bool>
{
    private readonly IVenueService _venueService;

    public DeleteVenueCommandHandler(IVenueService venueService)
    {
        _venueService = venueService;
    }

    public async Task<bool> Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
    {
        return await _venueService.DeleteAsync(request.Id);
    }
}