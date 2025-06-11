using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Car;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Car.Queries;

public class GetCarByIdQueryHandler(ICarService carService) : IRequestHandler<GetCarByIdQuery, CarDto>
{
    public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        return await carService.GetByIdAsync(request.Id);
    }
}