using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerService _customerService;

    public CreateCustomerCommandHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerService.CreateAsync(request.Customer);
    }
}