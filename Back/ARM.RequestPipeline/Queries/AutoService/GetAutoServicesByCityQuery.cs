using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAutoServicesByCityQuery(string City) : IRequest<IEnumerable<BrandDto>>; 