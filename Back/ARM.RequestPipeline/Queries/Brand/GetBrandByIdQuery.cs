using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Brand;

public record GetBrandByIdQuery(string Id) : IRequest<BrandDto>;