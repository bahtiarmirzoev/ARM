using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Commands.Brand;

public record CreateBrandCommand(CreateBrandDto Brand) : IRequest<BrandDto>;