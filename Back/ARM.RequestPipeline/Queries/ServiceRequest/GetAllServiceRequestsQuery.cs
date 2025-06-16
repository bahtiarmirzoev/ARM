using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.ServiceRequest;

public record GetAllServiceRequestsQuery() : IRequest<IEnumerable<ServiceRequestDto>>;