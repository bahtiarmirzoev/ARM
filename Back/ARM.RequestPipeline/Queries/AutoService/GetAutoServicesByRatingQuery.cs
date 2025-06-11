using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAutoServicesByRatingQuery(decimal MinRating) : IRequest<IEnumerable<BrandDto>>; 