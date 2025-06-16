using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Customer;

public record GetCurrentCustomerQuery : IRequest<PublicCustomerDto>;