using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.RequestPipeline.Commands.Car;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Car.Commands;

public class UpdateCarCommandHandler(ICarService carService) : IRequestHandler<UpdateCarCommand, CarDto>
{
    public async Task<CarDto> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        return await carService.UpdateAsync(request.Id, request.Dto);
    }
}