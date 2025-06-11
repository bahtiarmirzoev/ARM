using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Commands;

public class DeleteAutoServiceCommandHandler(IBrandService brandService) 
    : IRequestHandler<DeleteAutoServiceCommand, bool>
{
    public async Task<bool> Handle(DeleteAutoServiceCommand request, CancellationToken cancellationToken)
    {
        return await brandService.DeleteAsync(request.Id);
    }
} 