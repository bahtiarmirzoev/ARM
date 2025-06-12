using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Commands;

public class CreateBrandCommandHandler(IBrandService brandService)
    : IRequestHandler<CreateBrandCommand, BrandDto>
{
    public async Task<BrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        return await brandService.CreateAsync(request.Brand);
    }
}