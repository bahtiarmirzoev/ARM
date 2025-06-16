using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly ICustomerService _customerService;

    public UpdateCustomerCommandHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerService.UpdateAsync(request.Id, request.Customer);
    }
}