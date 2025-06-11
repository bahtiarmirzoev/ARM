using MediatR;

namespace ARM.RequestPipeline.Commands.Car;

public record DeleteCarCommand(string Id) : IRequest<bool>;