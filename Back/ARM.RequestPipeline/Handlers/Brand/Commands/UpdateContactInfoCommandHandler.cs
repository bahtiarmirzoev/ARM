using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Brand;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Brand.Commands;

public class UpdateContactInfoCommandHandler(IBrandService brandService)
    : IRequestHandler<UpdateContactInfoCommand, bool>
{
    public async Task<bool> Handle(UpdateContactInfoCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateContactInfoAsync(request.Id, request.Phone, request.Email);
    }
}