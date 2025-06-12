using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.Brand;

public record GetNearbyBrandsQuery(double Latitude, double Longitude, double RadiusKm) : IRequest<IEnumerable<BrandDto>>;