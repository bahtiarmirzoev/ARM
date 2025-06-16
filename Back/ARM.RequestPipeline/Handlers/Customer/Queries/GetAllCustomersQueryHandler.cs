using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Queries;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerService _customerService;

    public GetAllCustomersQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerService.GetAllAsync();
    }
}