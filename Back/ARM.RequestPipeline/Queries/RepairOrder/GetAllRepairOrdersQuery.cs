using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.RepairOrder;

public record GetAllRepairOrdersQuery() : IRequest<IEnumerable<RepairOrderDto>>;