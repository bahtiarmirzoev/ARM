using MediatR;

namespace ARM.RequestPipeline.Commands.Brand;

public record UpdateContactInfoCommand(string Id, string Phone, string Email) : IRequest<bool>;