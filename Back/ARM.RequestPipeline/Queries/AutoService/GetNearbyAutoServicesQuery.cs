using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.AutoService;

public record GetNearbyAutoServicesQuery(double Latitude, double Longitude, double RadiusKm) : IRequest<IEnumerable<BrandDto>>; 