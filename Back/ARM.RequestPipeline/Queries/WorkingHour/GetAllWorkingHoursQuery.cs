using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.WorkingHour;

public record GetAllWorkingHoursQuery() : IRequest<IEnumerable<WorkingHourDto>>;