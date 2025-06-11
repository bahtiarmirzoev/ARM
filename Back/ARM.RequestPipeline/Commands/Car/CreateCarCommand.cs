using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Car;

public record CreateCarCommand(CreateCarDto Dto) : IRequest<CarDto>;