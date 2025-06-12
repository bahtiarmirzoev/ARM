using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Brand;

public record GetBrandsBySpecializationQuery(string Specialization) : IRequest<IEnumerable<BrandDto>>;