using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Review;

public record CreateReviewCommand(CreateReviewDto Review) : IRequest<ReviewDto>;