using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;
using FluentValidation;

namespace ARM.Application.Services.Main;

public class VenueService : IVenueService
{
    private readonly IMapper _mapper;
    private readonly IVenueRepository _venueRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IValidator<CreateVenueDto> _createVenueValidator;
    private readonly IValidator<UpdateVenueDto> _updateVenueValidator;
    private readonly IUnitOfWork _unitOfWork;

    public VenueService(
        IMapper mapper,
        IVenueRepository venueRepository,
        IBrandRepository brandRepository,
        IValidator<CreateVenueDto> createVenueValidator,
        IValidator<UpdateVenueDto> updateVenueValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _venueRepository = venueRepository;
        _brandRepository = brandRepository;
        _createVenueValidator = createVenueValidator;
        _updateVenueValidator = updateVenueValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<VenueDto> CreateAsync(CreateVenueDto dto)
    {
        await _createVenueValidator.ValidateAndThrowAsync(dto);

        if (!await _brandRepository.AnyAsync(b => b.Id == dto.BrandId))
            throw new AppException(ExceptionType.NotFound, "BrandNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var venueEntity = _mapper.Map<VenueEntity>(dto);
            await _venueRepository.AddAsync(venueEntity);
            return _mapper.Map<VenueDto>(venueEntity);
        });
    }

    public async Task<VenueDto> UpdateAsync(string id, UpdateVenueDto dto)
    {
        await _updateVenueValidator.ValidateAndThrowAsync(dto);

        if (!await _brandRepository.AnyAsync(b => b.Id == dto.BrandId))
            throw new AppException(ExceptionType.NotFound, "BrandNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var venueEntity = await _venueRepository.GetByIdAsync(id);
            _mapper.Map(dto, venueEntity);
            await _venueRepository.UpdateAsync(new[] { venueEntity });
            return _mapper.Map<VenueDto>(venueEntity);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _venueRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<VenueDto> GetByIdAsync(string id)
    {
        var venueEntity = await _venueRepository.GetByIdAsync(id);
        return _mapper.Map<VenueDto>(venueEntity);
    }

    public async Task<IEnumerable<VenueDto>> GetAllAsync()
    {
        var venueEntities = await _venueRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VenueDto>>(venueEntities);
    }

    public async Task<IEnumerable<VenueDto>> GetByBrandIdAsync(string brandId)
    {
        var venueEntities = await _venueRepository.FindAsync(v => v.BrandId == brandId);
        return _mapper.Map<IEnumerable<VenueDto>>(venueEntities);
    }

    public async Task<IEnumerable<VenueDto>> GetByLocationAsync(double latitude, double longitude, double radiusInKm)
    {
        const double EarthRadiusKm = 6371;

        var latDiff = radiusInKm / EarthRadiusKm * (180 / Math.PI);
        var lonDiff = radiusInKm / EarthRadiusKm * (180 / Math.PI) / Math.Cos(latitude * Math.PI / 180);

        var minLat = latitude - latDiff;
        var maxLat = latitude + latDiff;
        var minLon = longitude - lonDiff;
        var maxLon = longitude + lonDiff;

        var venuesInBox = await _venueRepository.FindAsync(v =>
            v.Latitude >= minLat && v.Latitude <= maxLat &&
            v.Longitude >= minLon && v.Longitude <= maxLon);

        var nearbyVenues = venuesInBox.Where(v =>
        {
            var distance = CalculateDistance(latitude, longitude, v.Latitude, v.Longitude);
            return distance <= radiusInKm;
        });

        return _mapper.Map<IEnumerable<VenueDto>>(nearbyVenues);
    }

    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double earthRadiusKm = 6371;
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadiusKm * c;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}