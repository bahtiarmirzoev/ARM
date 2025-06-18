using System.Security.Claims;
using System.Linq;
using ARM.Common.Exceptions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Common.Extensions;
using ARM.Core.Common;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using ARM.Core.Enums;
using NanoidDotNet;

namespace ARM.Application.Services.Main;

public class BrandService(
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IBrandRepository brandRepository,
    IServiceRepository serviceRepository,
    IVenueRepository venueRepository,
    IPermissionService permissionService,
    IValidator<CreateBrandDto> createAutoServiceValidator,
    IValidator<UpdateBrandDto> updateAutoServiceValidator,
    IUnitOfWork unitOfWork)
    : IBrandService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    private HttpContext context => httpContextAccessor.HttpContext;

    public async Task<BrandDto> CreateAsync(CreateBrandDto dto)
    {
        await permissionService.EnsurePermissionAsync("brand_create");
        await createAutoServiceValidator.ValidateAndThrowAsync(dto);
        var brandEntity = mapper.Map<BrandEntity>(dto);
        brandEntity.Services = new List<ServiceEntity>();

        foreach (var serviceDto in dto.Services)
        {
            var serviceEntity = new ServiceEntity
            {
                Id = Nanoid.Generate(size: 24),
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                Price = serviceDto.Price,
                Duration = serviceDto.Duration,
                IsActive = serviceDto.IsActive,
                Brand = brandEntity
            };

            brandEntity.Services.Add(serviceEntity);
        }

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await brandRepository.AddAsync(brandEntity);

            var defaultVenue = mapper.Map<VenueEntity>(brandEntity);
            defaultVenue.Id = Nanoid.Generate(size: 24);
            defaultVenue.Name = $"{brandEntity.Name} - Main";
            defaultVenue.BrandId = brandEntity.Id;
            defaultVenue.Brand = brandEntity;

            await venueRepository.AddAsync(defaultVenue);

            return mapper.Map<BrandDto>(brandEntity);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await permissionService.EnsurePermissionAsync("brand_delete");
        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await brandRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
    {
        var brands = await brandRepository.GetAllAsync();
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<PaginatedResponse<BrandDto>> GetBrandsPageAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new AppException(ExceptionType.BadRequest, "InvalidPaginationParameters");

        var brands = await brandRepository.GetAllAsync();
        var totalItems = brands.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagedBrands = brands
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResponse<BrandDto>
        {
            Data = mapper.Map<IEnumerable<BrandDto>>(pagedBrands),
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<BrandStatisticsDto> GetServiceStatisticsAsync(string brandId)
    {
        await permissionService.EnsurePermissionAsync("brand_view_statistics");
        var brands = await brandRepository.FindAsync(b => b.Id == brandId);
        var brand = brands.FirstOrDefault()
            .EnsureFound("BrandNotFound");

        var completedRequests = brand.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Completed);

        return new BrandStatisticsDto
        {
            TotalServices = brand.Services.Count,
            ActiveServices = brand.Services.Count(s => s.IsActive),
            TotalRequests = brand.Services.Sum(s => s.ServiceRequests.Count),
            CompletedRequests = completedRequests.Count(),
            TotalRevenue = completedRequests.Sum(r => r.Service.Price),
            AverageRevenue = completedRequests.Any() ? completedRequests.Average(r => r.Service.Price) : 0,
            AverageRating = brand.Rating,
            TotalReviews = brand.TotalReviews,
            IsOpen = brand.IsOpen,
            MaxCarsPerDay = brand.MaxCarsPerDay,
            Services = mapper.Map<ICollection<ServiceDto>>(brand.Services),
            WorkingHours = mapper.Map<ICollection<WorkingHourDto>>(brand.WorkingHours),
            Venues = mapper.Map<ICollection<VenueDto>>(brand.Venues)
        };
    }

    public async Task<BrandStatisticsDto> GetRevenueStatisticsAsync(string brandId, DateTime startDate, DateTime endDate)
    {
        await permissionService.EnsurePermissionAsync("brand_view_statistics");
        var brands = await brandRepository.FindAsync(b => b.Id == brandId);
        var brand = brands.FirstOrDefault()
            .EnsureFound("BrandNotFound");

        var completedRequests = brand.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Completed && r.UpdatedAt >= startDate && r.UpdatedAt <= endDate);

        return new BrandStatisticsDto
        {
            TotalServices = brand.Services.Count,
            ActiveServices = brand.Services.Count(s => s.IsActive),
            TotalRequests = brand.Services.Sum(s => s.ServiceRequests.Count),
            CompletedRequests = completedRequests.Count(),
            TotalRevenue = completedRequests.Sum(r => r.Service.Price),
            AverageRevenue = completedRequests.Any() ? completedRequests.Average(r => r.Service.Price) : 0,
            AverageRating = brand.Rating,
            TotalReviews = brand.TotalReviews,
            IsOpen = brand.IsOpen,
            MaxCarsPerDay = brand.MaxCarsPerDay,
            Services = mapper.Map<ICollection<ServiceDto>>(brand.Services),
            WorkingHours = mapper.Map<ICollection<WorkingHourDto>>(brand.WorkingHours),
            Venues = mapper.Map<ICollection<VenueDto>>(brand.Venues)
        };
    }

    public async Task<bool> BlockBrandAsync(string brandId, bool isBlocked)
    {
        await permissionService.EnsurePermissionAsync("brand_block_toggle");
        var brand = (await brandRepository.FindAsync(b => b.Id == brandId))
            .FirstOrDefault()
            .EnsureFound("BrandNotFound");

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            brand.IsOpen = !isBlocked;
            await brandRepository.UpdateAsync(new[] { brand });
            return true;
        });
    }

    public async Task<bool> VerifyBrandAsync(string brandId, bool isVerified)
    {
        await permissionService.EnsurePermissionAsync("brand_verify");
        var brand = (await brandRepository.FindAsync(b => b.Id == brandId))
            .FirstOrDefault()
            .EnsureFound("BrandNotFound");

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            brand.IsOpen = isVerified;
            await brandRepository.UpdateAsync(new[] { brand });
            return true;
        });
    }

    public async Task<Dictionary<string, object>> GetSystemStatisticsAsync()
    {
        await permissionService.EnsurePermissionAsync("system_statistics_view");
        var brands = await brandRepository.GetAllAsync();
        var brandsList = brands.ToList();
        var totalBrands = brandsList.Count;
        var activeBrands = brandsList.Count(b => b.IsOpen);
        var totalServices = brandsList.Sum(b => b.Services.Count);
        var totalRequests = brandsList.Sum(b => b.Services.Sum(s => s.ServiceRequests.Count));

        return new Dictionary<string, object>
        {
            { "TotalBrands", totalBrands },
            { "ActiveBrands", activeBrands },
            { "TotalServices", totalServices },
            { "TotalRequests", totalRequests }
        };
    }

    public async Task<bool> UpdateBrandStatusAsync(string brandId, string status)
    {
        await permissionService.EnsurePermissionAsync("brand_status_update");
        var brand = await brandRepository.GetByIdAsync(brandId);

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            brand.IsOpen = status == "Active";
            await brandRepository.UpdateAsync([brand]);
            return true;
        });
    }

    public async Task<IEnumerable<BrandDto>> GetBlockedBrandsAsync()
    {
        await permissionService.EnsurePermissionAsync("brand_view_blocked");
        var brands = await brandRepository.FindAsync(b => !b.IsOpen);
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<IEnumerable<BrandDto>> GetUnverifiedBrandsAsync()
    {
        await permissionService.EnsurePermissionAsync("brand_view_unverified");
        var brands = await brandRepository.FindAsync(b => !b.IsOpen);
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }
    public async Task<BrandDto> UpdateBrandAsync(string id, UpdateBrandDto dto)
    {
        await permissionService.EnsurePermissionAsync("admin_brand_update");
        var brand = await brandRepository.GetByIdAsync(id);

        await updateAutoServiceValidator.ValidateAndThrowAsync(dto);

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            mapper.Map(dto, brand);
            await brandRepository.UpdateAsync(new[] { brand });
            return mapper.Map<BrandDto>(brand);
        });
    }

    public async Task<BrandDetailedInfoDto> GetAdminBrandAsync()
    {
        await permissionService.EnsurePermissionAsync("admin_brand_view_details");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        return mapper.Map<BrandDetailedInfoDto>(brand);
    }

    public async Task<BrandStatisticsDto> GetAdminServiceStatisticsAsync()
    {
        await permissionService.EnsurePermissionAsync("admin_statistics_view");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        var completedRequests = brand.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Completed);

        return new BrandStatisticsDto
        {
            TotalServices = brand.Services.Count,
            ActiveServices = brand.Services.Count(s => s.IsActive),
            TotalRequests = brand.Services.Sum(s => s.ServiceRequests.Count),
            CompletedRequests = completedRequests.Count(),
            TotalRevenue = completedRequests.Sum(r => r.Service.Price),
            AverageRevenue = completedRequests.Any() ? completedRequests.Average(r => r.Service.Price) : 0,
            AverageRating = brand.Rating,
            TotalReviews = brand.TotalReviews,
            IsOpen = brand.IsOpen,
            MaxCarsPerDay = brand.MaxCarsPerDay,
            Services = mapper.Map<ICollection<ServiceDto>>(brand.Services),
            WorkingHours = mapper.Map<ICollection<WorkingHourDto>>(brand.WorkingHours),
            Venues = mapper.Map<ICollection<VenueDto>>(brand.Venues)
        };
    }

    public async Task<BrandStatisticsDto> GetAdminRevenueStatisticsAsync(DateTime startDate, DateTime endDate)
    {
        await permissionService.EnsurePermissionAsync("admin_statistics_view");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        var completedRequests = brand.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Completed && r.CreatedAt >= startDate && r.CreatedAt <= endDate);

        return new BrandStatisticsDto
        {
            TotalServices = brand.Services.Count,
            ActiveServices = brand.Services.Count(s => s.IsActive),
            TotalRequests = completedRequests.Count(),
            CompletedRequests = completedRequests.Count(),
            TotalRevenue = completedRequests.Sum(r => r.Service.Price),
            AverageRevenue = completedRequests.Any() ? completedRequests.Average(r => r.Service.Price) : 0,
            AverageRating = brand.Rating,
            TotalReviews = brand.TotalReviews,
            IsOpen = brand.IsOpen,
            MaxCarsPerDay = brand.MaxCarsPerDay,
            Services = mapper.Map<ICollection<ServiceDto>>(brand.Services),
            WorkingHours = mapper.Map<ICollection<WorkingHourDto>>(brand.WorkingHours),
            Venues = mapper.Map<ICollection<VenueDto>>(brand.Venues)
        };
    }

    public async Task<bool> UpdateAdminWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime)
    {
        await permissionService.EnsurePermissionAsync("admin_working_hours_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        foreach (var workingHour in brand.WorkingHours)
        {
            workingHour.OpenTime = startTime;
            workingHour.CloseTime = endTime;
        }

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<bool> UpdateAdminContactInfoAsync(string phone, string email)
    {
        await permissionService.EnsurePermissionAsync("admin_contact_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        brand.PhoneNumber = phone;
        brand.Email = email;

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<bool> UpdateAdminAddressAsync(string address)
    {
        await permissionService.EnsurePermissionAsync("admin_address_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        brand.Address = address;

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<bool> UpdateAdminDescriptionAsync(string description)
    {
        await permissionService.EnsurePermissionAsync("admin_description_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        brand.Description = description;

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<bool> UpdateAdminCapacityAsync(int maxConcurrentServices)
    {
        await permissionService.EnsurePermissionAsync("admin_capacity_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        brand.MaxCarsPerDay = maxConcurrentServices;

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<bool> ToggleAdminAvailabilityAsync(bool isAvailable)
    {
        await permissionService.EnsurePermissionAsync("admin_availability_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        brand.IsOpen = isAvailable;

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<bool> UpdateAdminServicesAsync(IEnumerable<ServiceEntity> services)
    {
        await permissionService.EnsurePermissionAsync("admin_services_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        brand.Services = services.ToList();

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }
    public async Task<BrandDetailedInfoDto> GetManagerBrandInfoAsync()
    {
        await permissionService.EnsurePermissionAsync("manager_brand_view_details");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        return mapper.Map<BrandDetailedInfoDto>(brand);
    }

    public async Task<bool> UpdateManagerWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime)
    {
        await permissionService.EnsurePermissionAsync("manager_working_hours_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        foreach (var workingHour in brand.WorkingHours)
        {
            workingHour.OpenTime = startTime;
            workingHour.CloseTime = endTime;
        }

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<bool> ToggleManagerAvailabilityAsync(bool isAvailable)
    {
        await permissionService.EnsurePermissionAsync("manager_availability_update");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        brand.IsOpen = isAvailable;

        await brandRepository.UpdateAsync(new[] { brand });
        return true;
    }

    public async Task<BrandStatisticsDto> GetManagerDailyStatisticsAsync()
    {
        await permissionService.EnsurePermissionAsync("manager_statistics_view");
        var brandId = context.GetBrandId();
        var brand = await brandRepository.GetByIdAsync(brandId);

        var today = DateTime.UtcNow.Date;
        var completedRequests = brand.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Completed && r.CreatedAt.Date == today);

        return new BrandStatisticsDto
        {
            TotalServices = brand.Services.Count,
            ActiveServices = brand.Services.Count(s => s.IsActive),
            TotalRequests = completedRequests.Count(),
            CompletedRequests = completedRequests.Count(),
            TotalRevenue = completedRequests.Sum(r => r.Service.Price),
            AverageRevenue = completedRequests.Any() ? completedRequests.Average(r => r.Service.Price) : 0,
            AverageRating = brand.Rating,
            TotalReviews = brand.TotalReviews,
            IsOpen = brand.IsOpen,
            MaxCarsPerDay = brand.MaxCarsPerDay,
            Services = mapper.Map<ICollection<ServiceDto>>(brand.Services),
            WorkingHours = mapper.Map<ICollection<WorkingHourDto>>(brand.WorkingHours),
            Venues = mapper.Map<ICollection<VenueDto>>(brand.Venues)
        };
    }
    public async Task<IEnumerable<BrandDto>> GetAvailableBrandsAsync()
    {
        var brands = await brandRepository.FindAsync(b => b.IsOpen);
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsByCityAsync(string city)
    {
        var brands = await brandRepository.FindAsync(b => b.Address.Contains(city, StringComparison.OrdinalIgnoreCase));
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsByRatingAsync(decimal minRating)
    {
        var brands = await brandRepository.FindAsync(b => b.Rating >= minRating);
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsByWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime)
    {
        var brands = await brandRepository.FindAsync(b =>
            b.WorkingHours.Any(w => w.OpenTime <= startTime && w.CloseTime >= endTime));
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var brands = await brandRepository.FindAsync(b =>
            b.Services.Any(s => s.Price >= minPrice && s.Price <= maxPrice));
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<IEnumerable<BrandDto>> GetPopularBrandsAsync(int limit = 10)
    {
        var brands = await brandRepository.GetAllAsync();
        var popularBrands = brands
            .OrderByDescending(b => b.Services.Sum(s => s.ServiceRequests.Count))
            .Take(limit);

        return mapper.Map<IEnumerable<BrandDto>>(popularBrands);
    }

    public async Task<BrandDetailedInfoDto> GetBrandByIdAsync(string id)
    {
        var brand = await brandRepository.GetByIdAsync(id);
        return mapper.Map<BrandDetailedInfoDto>(brand);
    }

    public async Task<IEnumerable<BrandDto>> GetVerifiedBrandsAsync()
    {
        var brands = await brandRepository.FindAsync(b => b.IsOpen);
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDetailedInfoDto> GetBrandDetailedInfoAsync(string brandId)
    {
        var brand = await brandRepository.GetByIdAsync(brandId);
        return mapper.Map<BrandDetailedInfoDto>(brand);
    }
}