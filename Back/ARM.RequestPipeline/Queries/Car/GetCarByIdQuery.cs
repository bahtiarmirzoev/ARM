using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Car;

public record GetCarByIdQuery(string Id) : IRequest<CarDto>;