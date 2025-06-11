using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.AutoService;

public record CreateAutoServiceCommand(CreateBrandDto Brand) : IRequest<BrandDto>; 