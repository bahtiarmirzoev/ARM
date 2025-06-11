using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Commands;

public class UpdateAutoServiceCommandHandler(IBrandService brandService) 
    : IRequestHandler<UpdateAutoServiceCommand, BrandDto>
{
    public async Task<BrandDto> Handle(UpdateAutoServiceCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateAsync(request.Id, request.Brand);
    }
} 