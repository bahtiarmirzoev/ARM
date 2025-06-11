using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record UpdateContactInfoCommand(string AutoServiceId, string Phone, string Email) : IRequest<bool>; 