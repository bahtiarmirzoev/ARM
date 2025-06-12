using System.Security.Claims;
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

public class BrandService : IBrandService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBrandRepository _brandRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IVenueRepository _venueRepository;
    private readonly IValidator<CreateBrandDto> _createAutoServiceValidator;
    private readonly IValidator<UpdateBrandDto> _updateAutoServiceValidator;
    private readonly IUnitOfWork _unitOfWork;

    public BrandService(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IBrandRepository brandRepository,
        IServiceRepository serviceRepository,
        IVenueRepository venueRepository,
        IValidator<CreateBrandDto> createAutoServiceValidator,
        IValidator<UpdateBrandDto> updateAutoServiceValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _brandRepository = brandRepository;
        _serviceRepository = serviceRepository;
        _venueRepository = venueRepository;
        _createAutoServiceValidator = createAutoServiceValidator;
        _updateAutoServiceValidator = updateAutoServiceValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<BrandDto> CreateAsync(CreateBrandDto dto)
    {
        await _createAutoServiceValidator.ValidateAndThrowAsync(dto);
        var autoServiceEntity = _mapper.Map<BrandEntity>(dto);
        autoServiceEntity.Services = new List<ServiceEntity>();

        foreach (var serviceDto in dto.Services)
        {
            var serviceEntity = new ServiceEntity
            {
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                Price = serviceDto.Price,
                Duration = serviceDto.Duration,
                IsActive = serviceDto.IsActive,
                Brand = autoServiceEntity
            };

            autoServiceEntity.Services.Add(serviceEntity);
        }

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _brandRepository.AddAsync(autoServiceEntity);
            
            if (dto.Venues.Any())
            {
                foreach (var venueDto in dto.Venues)
                {
                    var venueEntity = _mapper.Map<VenueEntity>(venueDto);
                    venueEntity.BrandId = autoServiceEntity.Id;
                    venueEntity.Brand = autoServiceEntity;
                    venueEntity.Services = autoServiceEntity.Services;
                    await _venueRepository.AddAsync(venueEntity);
                }
            }

            return _mapper.Map<BrandDto>(autoServiceEntity);
        });
    }

    public async Task<BrandDto> UpdateAsync(string id, UpdateBrandDto dto)
    {
        var existingAutoService = (await _brandRepository.FindAsync(
                a => a.Id == id))
            .FirstOrDefault()
            .EnsureFound("AutoServiceNotFound");

        await _updateAutoServiceValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingAutoService);
            await _brandRepository.UpdateAsync(new[] { existingAutoService });

            return _mapper.Map<BrandDto>(existingAutoService);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var autoService = (await _brandRepository.FindAsync(
                a => a.Id == id))
            .FirstOrDefault()
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _brandRepository.DeleteAsync(autoService.Id);
            return true;
        });
    }

    public async Task<BrandDto> GetByIdAsync(string id)
    {
        var autoService = await _brandRepository.GetByIdAsync(id);
        return _mapper.Map<BrandDto>(autoService);
    }

    public async Task<IEnumerable<BrandDto>> GetAllAsync()
    {
        var autoServices = await _brandRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BrandDto>>(autoServices);
    }

    public async Task<PaginatedResponse<BrandDto>> GetPageAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new AppException(ExceptionType.BadRequest, "PaginationError");

        var totalAutoServices = await _brandRepository.GetAllAsync();
        var totalItems = totalAutoServices.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagedAutoServices = totalAutoServices
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var autoServiceDtos = _mapper.Map<IEnumerable<BrandDto>>(pagedAutoServices);

        return new PaginatedResponse<BrandDto>
        {
            Data = autoServiceDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<BrandDto>> GetByCityAsync(string city)
    {
        var autoServices = await _brandRepository.FindAsync(a => a.Address.Contains(city));
        return _mapper.Map<IEnumerable<BrandDto>>(autoServices);
    }

    public async Task<IEnumerable<BrandDto>> GetByRatingAsync(decimal minRating)
    {
        var autoServices = await _brandRepository.FindAsync(a => a.Rating >= minRating);
        return _mapper.Map<IEnumerable<BrandDto>>(autoServices);
    }

    public async Task<IEnumerable<BrandDto>> GetByWorkingHoursAsync(TimeSpan startTime, TimeSpan endTime)
    {
        var autoServices = await _brandRepository.FindAsync(a =>
            a.WorkingHours.Any(w => w.OpenTime <= startTime && w.CloseTime >= endTime));
        return _mapper.Map<IEnumerable<BrandDto>>(autoServices);
    }


    public async Task<IEnumerable<BrandDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var autoServices = await _brandRepository.FindAsync(a =>
            a.Services.Any(s => s.Price >= minPrice && s.Price <= maxPrice));
        return _mapper.Map<IEnumerable<BrandDto>>(autoServices);
    }

    public async Task<Dictionary<string, int>> GetServiceStatisticsAsync(string autoServiceId)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return new Dictionary<string, int>
        {
            { "TotalServices", autoService.Services.Count },
            { "ActiveServices", autoService.Services.Count(s => s.IsActive) },
            { "TotalRequests", autoService.Services.Sum(s => s.ServiceRequests.Count) },
            { "CompletedRequests", autoService.Services.Sum(s => s.ServiceRequests.Count(r => r.Status == RequestStatus.Completed)) }
        };
    }

    public async Task<Dictionary<string, decimal>> GetRevenueStatisticsAsync(string autoServiceId, DateTime startDate, DateTime endDate)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        var completedRequests = autoService.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Completed && r.UpdatedAt >= startDate && r.UpdatedAt <= endDate);

        return new Dictionary<string, decimal>
        {
            { "TotalRevenue", completedRequests.Sum(r => r.Service.Price) },
            { "AverageRevenue", completedRequests.Any() ? completedRequests.Average(r => r.Service.Price) : 0 }
        };
    }

    public async Task<IEnumerable<BrandDto>> GetPopularServicesAsync(int limit = 6)
    {
        var autoServices = await _brandRepository.GetAllAsync();
        var popularServices = autoServices
            .OrderByDescending(a => a.Services.Sum(s => s.ServiceRequests.Count))
            .Take(limit);

        return _mapper.Map<IEnumerable<BrandDto>>(popularServices);
    }

    public async Task<bool> UpdateWorkingHoursAsync(string autoServiceId, TimeSpan startTime, TimeSpan endTime)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            autoService.WorkingHours.Add(new WorkingHourEntity
            {
                OpenTime = startTime,
                CloseTime = endTime,
                Day = DayOfWeek.Monday
            });
            await _brandRepository.UpdateAsync(new[] { autoService });
            return true;
        });
    }

    public async Task<bool> UpdateContactInfoAsync(string autoServiceId, string phone, string email)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            autoService.PhoneNumber = phone;
            autoService.Email = email;
            await _brandRepository.UpdateAsync(new[] { autoService });
            return true;
        });
    }

    public async Task<bool> UpdateAddressAsync(string autoServiceId, string address)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            autoService.Address = address;
            await _brandRepository.UpdateAsync(new[] { autoService });
            return true;
        });
    }

    public async Task<bool> UpdateDescriptionAsync(string autoServiceId, string description)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            autoService.Description = description;
            await _brandRepository.UpdateAsync(new[] { autoService });
            return true;
        });
    }

    public async Task<bool> UpdateRatingAsync(string autoServiceId, decimal newRating)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            autoService.Rating = newRating;
            await _brandRepository.UpdateAsync(new[] { autoService });
            return true;
        });
    }

    public async Task<IEnumerable<BrandDto>> GetNearbyServicesAsync(double latitude, double longitude, double radiusKm)
    {
        var autoServices = await _brandRepository.GetAllAsync();
        var nearbyServices = autoServices
            .Where(a => CalculateDistance(latitude, longitude, a.Latitude, a.Longitude) <= radiusKm)
            .OrderBy(a => CalculateDistance(latitude, longitude, a.Latitude, a.Longitude));

        return _mapper.Map<IEnumerable<BrandDto>>(nearbyServices);
    }

    public async Task<Dictionary<string, int>> GetTechnicianWorkloadAsync(string autoServiceId)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        var activeRequests = autoService.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Processed || r.Status == RequestStatus.Confirmed);

        return activeRequests
            .GroupBy(r => r.UserId)
            .ToDictionary(
                g => g.Key,
                g => g.Count()
            );
    }

    public async Task<bool> UpdateCapacityAsync(string autoServiceId, int maxConcurrentServices)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            autoService.MaxCarsPerDay = maxConcurrentServices;
            await _brandRepository.UpdateAsync(new[] { autoService });
            return true;
        });
    }

    public async Task<Dictionary<string, decimal>> GetAverageServiceTimeAsync(string autoServiceId)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        var completedRequests = autoService.Services
            .SelectMany(s => s.ServiceRequests)
            .Where(r => r.Status == RequestStatus.Completed && r.UpdatedAt.HasValue);

        return new Dictionary<string, decimal>
        {
            { "AverageServiceTime", completedRequests.Any()
                ? (decimal)completedRequests.Average(r => (r.UpdatedAt!.Value - r.CreatedAt).TotalHours)
                : 0 }
        };
    }

    public async Task<IEnumerable<BrandDto>> GetByAvailabilityAsync(bool isAvailable)
    {
        var autoServices = await _brandRepository.FindAsync(a => a.IsOpen == isAvailable);
        return _mapper.Map<IEnumerable<BrandDto>>(autoServices);
    }

    public async Task<bool> ToggleAvailabilityAsync(string autoServiceId, bool isAvailable)
    {
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId)
            .EnsureFound("AutoServiceNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            autoService.IsOpen = isAvailable;
            await _brandRepository.UpdateAsync(new[] { autoService });
            return true;
        });
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadiusKm = 6371;
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }

    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}