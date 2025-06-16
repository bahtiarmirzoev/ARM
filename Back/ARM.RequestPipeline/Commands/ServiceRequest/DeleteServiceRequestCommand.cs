using MediatR;

namespace ARM.RequestPipeline.Commands.ServiceRequest;

public record DeleteServiceRequestCommand(string Id) : IRequest<bool>;