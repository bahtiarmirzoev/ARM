using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAutoServicesByServiceTypeQuery(string ServiceTypeId) : IRequest<IEnumerable<BrandDto>>; 