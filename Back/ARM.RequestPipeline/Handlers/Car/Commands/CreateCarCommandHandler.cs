using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Car;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Car.Commands;

public class CreateCarCommandHandler(ICarService carService) : IRequestHandler<CreateCarCommand, CarDto>
{
    public async Task<CarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        return await carService.CreateAsync(request.Dto);
    }
}