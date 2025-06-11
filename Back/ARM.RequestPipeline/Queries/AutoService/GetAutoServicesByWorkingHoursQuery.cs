using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetAutoServicesByWorkingHoursQuery(TimeSpan StartTime, TimeSpan EndTime) : IRequest<IEnumerable<BrandDto>>; 