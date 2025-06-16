using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Queries;

public class GetCurrentCustomerHandler(ICustomerService customerService) : IRequestHandler<GetCurrentCustomerQuery, PublicCustomerDto>
{
    public async Task<PublicCustomerDto> Handle(GetCurrentCustomerQuery request, CancellationToken cancellationToken)
    {
        return await customerService.GetCurrentCustomerAsync();
    }
}