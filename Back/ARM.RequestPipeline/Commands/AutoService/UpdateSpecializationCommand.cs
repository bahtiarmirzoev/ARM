using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record UpdateSpecializationCommand(string AutoServiceId, string Specialization) : IRequest<bool>; 