using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Brand;

public record GetBrandsByRatingQuery(decimal MinRating) : IRequest<IEnumerable<BrandDto>>;