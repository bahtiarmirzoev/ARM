using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Commands;

public class CreateAutoServiceCommandHandler(IBrandService brandService) 
    : IRequestHandler<CreateAutoServiceCommand, BrandDto>
{
    public async Task<BrandDto> Handle(CreateAutoServiceCommand request, CancellationToken cancellationToken)
    {
        return await brandService.CreateAsync(request.Brand);
    }
} 