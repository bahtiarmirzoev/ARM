using MediatR;

namespace ARM.RequestPipeline.Commands.Review;

public record DeleteReviewCommand(string Id) : IRequest<bool>;