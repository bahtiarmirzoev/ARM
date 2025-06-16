using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.Review;

public record UpdateReviewCommand(string Id, UpdateReviewDto Review) : IRequest<ReviewDto>;