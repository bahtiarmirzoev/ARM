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

namespace ARM.Application.Services.Main;

public class CarService : ICarService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICarRepository _carRepository;
    private readonly IValidator<CreateCarDto> _createCarValidator;
    private readonly IValidator<UpdateCarDto> _updateCarValidator;
    private readonly IUnitOfWork _unitOfWork;

    public CarService(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ICarRepository carRepository,
        IValidator<CreateCarDto> createCarValidator,
        IValidator<UpdateCarDto> updateCarValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _carRepository = carRepository;
        _createCarValidator = createCarValidator;
        _updateCarValidator = updateCarValidator;
        _unitOfWork = unitOfWork;
    }

    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    public async Task<CarDto> CreateAsync(CreateCarDto dto)
    {
        var userId = GetUserId();
        await _createCarValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var carEntity = _mapper.Map<CarEntity>(dto);
            carEntity.OwnerId = userId;
            
            await _carRepository.AddAsync(carEntity);
            
            return _mapper.Map<CarDto>(carEntity);
        });
    }

    public async Task<CarDto> UpdateAsync(string id, UpdateCarDto dto)
    {
        var userId = GetUserId();
        var existingCar = (await _carRepository.FindAsync(
                c => c.Id == id && c.OwnerId == userId))
            .FirstOrDefault()
            .EnsureFound("CarNotFound");
        
        await _updateCarValidator.ValidateAndThrowAsync(dto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingCar);
            await _carRepository.UpdateAsync(new[] { existingCar });
            
            return _mapper.Map<CarDto>(existingCar);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var userId = GetUserId();
        var car = (await _carRepository.FindAsync(
                c => c.Id == id && c.OwnerId == userId))
            .FirstOrDefault()
            .EnsureFound("CarNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _carRepository.DeleteAsync(car.Id);
            return true;
        });
    }

    public async Task<CarDto> GetByIdAsync(string id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        return _mapper.Map<CarDto>(car);
    }

    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        var userId = GetUserId();
        var cars = await _carRepository.FindAsync(u => u.OwnerId == userId);
        return _mapper.Map<IEnumerable<CarDto>>(cars);
    }
} 