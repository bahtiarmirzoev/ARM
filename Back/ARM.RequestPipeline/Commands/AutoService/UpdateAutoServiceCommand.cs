using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record UpdateAutoServiceCommand(string Id, UpdateBrandDto Brand) : IRequest<BrandDto>; 