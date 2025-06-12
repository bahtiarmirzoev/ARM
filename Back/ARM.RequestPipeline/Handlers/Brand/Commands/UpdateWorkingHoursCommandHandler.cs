using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Commands;

public class UpdateWorkingHoursCommandHandler(IBrandService brandService)
    : IRequestHandler<UpdateWorkingHoursCommand, bool>
{
    public async Task<bool> Handle(UpdateWorkingHoursCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateWorkingHoursAsync(request.Id, request.StartTime, request.EndTime);
    }
}