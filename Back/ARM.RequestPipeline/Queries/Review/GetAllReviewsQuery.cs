using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Review;

public record GetAllReviewsQuery() : IRequest<IEnumerable<ReviewDto>>;