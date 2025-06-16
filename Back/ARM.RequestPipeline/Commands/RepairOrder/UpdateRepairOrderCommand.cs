using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.RepairOrder;

public record UpdateRepairOrderCommand(string Id, UpdateRepairOrderDto RepairOrder) : IRequest<RepairOrderDto>;