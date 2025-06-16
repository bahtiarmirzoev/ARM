using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.RepairOrder;
using MediatR;

namespace ARM.RequestPipeline.Handlers.RepairOrder.Commands;

public class DeleteRepairOrderCommandHandler : IRequestHandler<DeleteRepairOrderCommand, bool>
{
    private readonly IRepairOrderService _repairOrderService;

    public DeleteRepairOrderCommandHandler(IRepairOrderService repairOrderService)
    {
        _repairOrderService = repairOrderService;
    }

    public async Task<bool> Handle(DeleteRepairOrderCommand request, CancellationToken cancellationToken)
    {
        return await _repairOrderService.DeleteAsync(request.Id);
    }
}