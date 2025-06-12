using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Brand;

public record GetBrandsByCityQuery(string City) : IRequest<IEnumerable<BrandDto>>;