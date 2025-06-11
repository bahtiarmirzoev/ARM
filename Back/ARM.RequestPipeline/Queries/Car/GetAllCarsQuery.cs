using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Car;

public record GetAllCarsQuery : IRequest<IEnumerable<CarDto>>;