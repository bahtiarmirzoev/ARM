using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAutoServicesBySpecializationQuery(string Specialization) : IRequest<IEnumerable<BrandDto>>; 