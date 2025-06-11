using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.AutoService;
using MediatR;

namespace ARM.RequestPipeline.Handlers.AutoService.Commands;

public class UpdateContactInfoCommandHandler(IBrandService brandService) 
    : IRequestHandler<UpdateContactInfoCommand, bool>
{
    public async Task<bool> Handle(UpdateContactInfoCommand request, CancellationToken cancellationToken)
    {
        return await brandService.UpdateContactInfoAsync(request.AutoServiceId, request.Phone, request.Email);
    }
} 