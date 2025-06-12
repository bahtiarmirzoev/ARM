using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Commands;

public class DeleteBrandCommandHandler(IBrandService brandService)
    : IRequestHandler<DeleteBrandCommand, bool>
{
    public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        return await brandService.DeleteAsync(request.Id);
    }
}