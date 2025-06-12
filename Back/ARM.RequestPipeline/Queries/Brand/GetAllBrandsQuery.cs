using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Brand;

public record GetAllBrandsQuery() : IRequest<IEnumerable<BrandDto>>;