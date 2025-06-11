using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Commands;

public class UpdateWorkingHoursCommandHandler(IBrandService brandService) 
    : IRequestHandler<UpdateWorkingHoursCommand, bool>
{
    public async Task<bool> Handle(UpdateWorkingHoursCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateWorkingHoursAsync(request.AutoServiceId, request.StartTime, request.EndTime);
    }
} 