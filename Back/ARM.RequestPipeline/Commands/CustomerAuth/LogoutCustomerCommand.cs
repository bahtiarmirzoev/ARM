using MediatR;

namespace ARM.RequestPipeline.Commands.CustomerAuth;

public record LogoutCustomerCommand() : IRequest<bool>;