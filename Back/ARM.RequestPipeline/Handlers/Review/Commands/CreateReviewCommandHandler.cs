using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Review;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Review.Commands;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IReviewService _reviewService;

    public CreateReviewCommandHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        return await _reviewService.CreateAsync(request.Review);
    }
}