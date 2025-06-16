using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Review;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Review.Commands;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
{
    private readonly IReviewService _reviewService;

    public UpdateReviewCommandHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        return await _reviewService.UpdateAsync(request.Id, request.Review);
    }
}