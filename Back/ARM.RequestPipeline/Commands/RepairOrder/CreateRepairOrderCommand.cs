using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.RepairOrder;

public record CreateRepairOrderCommand(CreateRepairOrderDto RepairOrder) : IRequest<RepairOrderDto>;