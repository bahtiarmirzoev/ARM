using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.RepairOrder;
using MediatR;

namespace ARM.RequestPipeline.Handlers.RepairOrder.Commands;

public class CreateRepairOrderCommandHandler : IRequestHandler<CreateRepairOrderCommand, RepairOrderDto>
{
    private readonly IRepairOrderService _repairOrderService;

    public CreateRepairOrderCommandHandler(IRepairOrderService repairOrderService)
    {
        _repairOrderService = repairOrderService;
    }

    public async Task<RepairOrderDto> Handle(CreateRepairOrderCommand request, CancellationToken cancellationToken)
    {
        return await _repairOrderService.CreateAsync(request.RepairOrder);
    }
}