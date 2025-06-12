using MediatR;

namespace ARM.RequestPipeline.Commands.Brand;

public record DeleteBrandCommand(string Id) : IRequest<bool>;