using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record DeleteAutoServiceCommand(string Id) : IRequest<bool>; 