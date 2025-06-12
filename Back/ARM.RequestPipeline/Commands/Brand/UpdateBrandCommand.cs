using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using MediatR;

namespace ARM.RequestPipeline.Commands.Brand;

public record UpdateBrandCommand(string Id, UpdateBrandDto Brand) : IRequest<BrandDto>;