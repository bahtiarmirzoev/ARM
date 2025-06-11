using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAutoServiceByIdQuery(string Id) : IRequest<BrandDto>; 