using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.Car;

public record UpdateCarCommand(string Id, UpdateCarDto Dto) : IRequest<CarDto>;