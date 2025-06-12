using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Commands;

public class UpdateBrandCommandHandler(IBrandService brandService)
    : IRequestHandler<UpdateBrandCommand, BrandDto>
{
    public async Task<BrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateAsync(request.Id, request.Brand);
    }
}