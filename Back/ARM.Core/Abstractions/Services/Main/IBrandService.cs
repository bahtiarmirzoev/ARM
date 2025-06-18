using ARM.Core.Common;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface IBrandService
{
    // Методы для SuperAdmin
    Task<BrandDto> CreateAsync(CreateBrandDto dto);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    Task<PaginatedResponse<BrandDto>> GetBrandsPageAsync(int pageNumber, int pageSize);
    Task<BrandStatisticsDto> GetServiceStatisticsAsync(string brandId);
    Task<BrandStatisticsDto> GetRevenueStatisticsAsync(string brandId, DateTime startDate, DateTime endDate);
    Task<bool> BlockBrandAsync(string brandId, bool isBlocked);
    Task<bool> VerifyBrandAsync(string brandId, bool isVerified);
    Task<Dictionary<string, object>> GetSystemStatisticsAsync();
    Task<bool> UpdateBrandStatusAsync(string brandId, string status);
    Task<IEnumerable<BrandDto>> GetBlockedBrandsAsync();
    Task<IEnumerable<BrandDto>> GetUnverifiedBrandsAsync();

    // Методы для Admin (владелец бренда)
    Task<BrandDto> UpdateBrandAsync(string id, UpdateBrandDto dto);
    Task<BrandDetailedInfoDto> GetAdminBrandAsync();
    Task<BrandStatisticsDto> GetAdminServiceStatisticsAsync();
    Task<BrandStatisticsDto> GetAdminRevenueStatisticsAsync(DateTime startDate, DateTime endDate);
    Task<bool> UpdateAdminWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime);
    Task<bool> UpdateAdminContactInfoAsync(string phone, string email);
    Task<bool> UpdateAdminAddressAsync(string address);
    Task<bool> UpdateAdminDescriptionAsync(string description);
    Task<bool> UpdateAdminCapacityAsync(int maxConcurrentServices);
    Task<bool> ToggleAdminAvailabilityAsync(bool isAvailable);
    Task<bool> UpdateAdminServicesAsync(IEnumerable<ServiceEntity> services);

    // Методы для Manager
    Task<BrandDetailedInfoDto> GetManagerBrandInfoAsync();
    Task<bool> UpdateManagerWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime);
    Task<bool> ToggleManagerAvailabilityAsync(bool isAvailable);
    Task<BrandStatisticsDto> GetManagerDailyStatisticsAsync();

    // Методы для клиентов
    Task<IEnumerable<BrandDto>> GetAvailableBrandsAsync();
    Task<IEnumerable<BrandDto>> GetBrandsByCityAsync(string city);
    Task<IEnumerable<BrandDto>> GetBrandsByRatingAsync(decimal minRating);
    Task<IEnumerable<BrandDto>> GetBrandsByWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime);
    Task<IEnumerable<BrandDto>> GetBrandsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<BrandDto>> GetPopularBrandsAsync(int limit = 10);
    Task<BrandDetailedInfoDto> GetBrandByIdAsync(string id);
    Task<IEnumerable<BrandDto>> GetVerifiedBrandsAsync();
    Task<BrandDetailedInfoDto> GetBrandDetailedInfoAsync(string brandId);
}