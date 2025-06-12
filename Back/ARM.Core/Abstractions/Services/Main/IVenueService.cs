using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;

namespace ARM.Core.Abstractions.Services.Main;

public interface IVenueService
{
    Task<VenueDto> CreateAsync(CreateVenueDto dto);
    Task<VenueDto> UpdateAsync(string id, UpdateVenueDto dto);
    Task<bool> DeleteAsync(string id);
    Task<VenueDto> GetByIdAsync(string id);
    Task<IEnumerable<VenueDto>> GetAllAsync();
    Task<IEnumerable<VenueDto>> GetByBrandIdAsync(string brandId);
    Task<IEnumerable<VenueDto>> GetByLocationAsync(double latitude, double longitude, double radiusInKm);
}