using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Review;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Review.Queries;

public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewDto>>
{
    private readonly IReviewService _reviewService;

    public GetAllReviewsQueryHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<IEnumerable<ReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _reviewService.GetAllAsync();
    }
}