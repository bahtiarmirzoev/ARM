using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.RepairOrder;
using MediatR;

namespace ARM.RequestPipeline.Handlers.RepairOrder.Queries;

public class GetAllRepairOrdersQueryHandler : IRequestHandler<GetAllRepairOrdersQuery, IEnumerable<RepairOrderDto>>
{
    private readonly IRepairOrderService _repairOrderService;

    public GetAllRepairOrdersQueryHandler(IRepairOrderService repairOrderService)
    {
        _repairOrderService = repairOrderService;
    }

    public async Task<IEnumerable<RepairOrderDto>> Handle(GetAllRepairOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _repairOrderService.GetAllAsync();
    }
}