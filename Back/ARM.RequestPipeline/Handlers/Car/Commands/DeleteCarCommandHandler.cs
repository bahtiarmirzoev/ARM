using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Car;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Car.Commands;

public class DeleteCarCommandHandler(ICarService carService) : IRequestHandler<DeleteCarCommand, bool>
{
    public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        return await carService.DeleteAsync(request.Id);
    }
}