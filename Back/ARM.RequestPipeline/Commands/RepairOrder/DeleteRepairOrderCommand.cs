using MediatR;

namespace ARM.RequestPipeline.Commands.RepairOrder;

public record DeleteRepairOrderCommand(string Id) : IRequest<bool>;