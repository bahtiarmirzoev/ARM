using ARM.Core.Common;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface IBrandService
{
    Task<BrandDto> CreateAsync(CreateBrandDto dto);
    Task<BrandDto> UpdateAsync(string id, UpdateBrandDto dto);
    Task<bool> DeleteAsync(string id);
    Task<BrandDto> GetByIdAsync(string id);
    Task<IEnumerable<BrandDto>> GetAllAsync();
    Task<PaginatedResponse<BrandDto>> GetPageAsync(int pageNumber, int pageSize);
    Task<IEnumerable<BrandDto>> GetByCityAsync(string city);
    Task<IEnumerable<BrandDto>> GetByRatingAsync(decimal minRating);
    Task<IEnumerable<BrandDto>> GetByWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime);
    Task<IEnumerable<BrandDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<Dictionary<string, int>> GetServiceStatisticsAsync(string autoServiceId);
    Task<Dictionary<string, decimal>> GetRevenueStatisticsAsync(string autoServiceId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<BrandDto>> GetPopularServicesAsync(int limit = 10);
    Task<bool> UpdateWorkingHoursAsync(string autoServiceId, TimeSpan startTime, TimeSpan endTime);
    Task<bool> UpdateContactInfoAsync(string autoServiceId, string phone, string email);
    Task<bool> UpdateAddressAsync(string autoServiceId, string address);
    Task<bool> UpdateDescriptionAsync(string autoServiceId, string description);
    Task<bool> UpdateRatingAsync(string autoServiceId, decimal newRating);
    Task<IEnumerable<BrandDto>> GetNearbyServicesAsync(double latitude, double longitude, double radiusKm);
    Task<Dictionary<string, int>> GetTechnicianWorkloadAsync(string autoServiceId);
    Task<bool> UpdateCapacityAsync(string autoServiceId, int maxConcurrentServices);
    Task<Dictionary<string, decimal>> GetAverageServiceTimeAsync(string autoServiceId);
    Task<IEnumerable<BrandDto>> GetByAvailabilityAsync(bool isAvailable);
    Task<bool> ToggleAvailabilityAsync(string autoServiceId, bool isAvailable);
} 