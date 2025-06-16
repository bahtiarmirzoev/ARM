using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly ICustomerService _customerService;

    public DeleteCustomerCommandHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerService.DeleteAsync(request.Id);
    }
}