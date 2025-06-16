using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.RepairOrder;
using MediatR;

namespace ARM.RequestPipeline.Handlers.RepairOrder.Commands;

public class UpdateRepairOrderCommandHandler : IRequestHandler<UpdateRepairOrderCommand, RepairOrderDto>
{
    private readonly IRepairOrderService _repairOrderService;

    public UpdateRepairOrderCommandHandler(IRepairOrderService repairOrderService)
    {
        _repairOrderService = repairOrderService;
    }

    public async Task<RepairOrderDto> Handle(UpdateRepairOrderCommand request, CancellationToken cancellationToken)
    {
        return await _repairOrderService.UpdateAsync(request.Id, request.RepairOrder);
    }
}