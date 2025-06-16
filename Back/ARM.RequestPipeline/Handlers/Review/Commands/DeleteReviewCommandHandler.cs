using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Review;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Review.Commands;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
{
    private readonly IReviewService _reviewService;

    public DeleteReviewCommandHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        return await _reviewService.DeleteAsync(request.Id);
    }
}