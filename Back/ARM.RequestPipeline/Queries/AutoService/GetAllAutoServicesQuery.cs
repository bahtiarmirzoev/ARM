using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAllAutoServicesQuery : IRequest<IEnumerable<BrandDto>>; 