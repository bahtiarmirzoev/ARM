using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Car;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Car.Queries;

public class GetAllCarsQueryHandler(ICarService carService) : IRequestHandler<GetAllCarsQuery, IEnumerable<CarDto>>
{
    public async Task<IEnumerable<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        return await carService.GetAllAsync();
    }
}