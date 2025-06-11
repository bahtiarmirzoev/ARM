using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAutoServicesByPriceRangeQuery(decimal MinPrice, decimal MaxPrice) : IRequest<IEnumerable<BrandDto>>; 