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

namespace ARM.Application.Services.Main;

public class RepairOrderService : IRepairOrderService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepairOrderRepository _repairOrderRepository;
    private readonly IValidator<CreateRepairOrderDto> _createRepairOrderValidator;
    private readonly IValidator<UpdateRepairOrderDto> _updateRepairOrderValidator;
    private readonly IUnitOfWork _unitOfWork;

    public RepairOrderService(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IRepairOrderRepository repairOrderRepository,
        IValidator<CreateRepairOrderDto> createRepairOrderValidator,
        IValidator<UpdateRepairOrderDto> updateRepairOrderValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _repairOrderRepository = repairOrderRepository;
        _createRepairOrderValidator = createRepairOrderValidator;
        _updateRepairOrderValidator = updateRepairOrderValidator;
        _unitOfWork = unitOfWork;
    }

    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    public async Task<RepairOrderDto> CreateAsync(CreateRepairOrderDto dto)
    {
        var userId = GetUserId();
        await _createRepairOrderValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var repairOrderEntity = _mapper.Map<RepairOrderEntity>(dto);
            repairOrderEntity.UserId = userId;
            repairOrderEntity.OrderDate = DateTime.UtcNow;
            repairOrderEntity.ServiceStatus = ServiceStatus.NotDone;
            
            await _repairOrderRepository.AddAsync(repairOrderEntity);
            
            return _mapper.Map<RepairOrderDto>(repairOrderEntity);
        });
    }

    public async Task<RepairOrderDto> UpdateAsync(string id, UpdateRepairOrderDto dto)
    {
        var userId = GetUserId();
        var existingOrder = (await _repairOrderRepository.FindAsync(
                o => o.Id == id && o.UserId == userId))
            .FirstOrDefault()
            .EnsureFound("RepairOrderNotFound");
        
        await _updateRepairOrderValidator.ValidateAndThrowAsync(dto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingOrder);
            await _repairOrderRepository.UpdateAsync(new[] { existingOrder });
            
            return _mapper.Map<RepairOrderDto>(existingOrder);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var userId = GetUserId();
        var order = (await _repairOrderRepository.FindAsync(
                o => o.Id == id && o.UserId == userId))
            .FirstOrDefault()
            .EnsureFound("RepairOrderNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _repairOrderRepository.DeleteAsync(order.Id);
            return true;
        });
    }

    public async Task<RepairOrderDto> GetByIdAsync(string id)
    {
        var order = await _repairOrderRepository.GetByIdAsync(id);
        return _mapper.Map<RepairOrderDto>(order);
    }

    public async Task<IEnumerable<RepairOrderDto>> GetAllAsync()
    {
        var userId = GetUserId();
        var orders = await _repairOrderRepository.FindAsync(u => u.UserId == userId);
        return _mapper.Map<IEnumerable<RepairOrderDto>>(orders);
    }

    public async Task<IEnumerable<RepairOrderDto>> GetByAutoServiceIdAsync(string autoServiceId)
    {
        var orders = await _repairOrderRepository.FindAsync(o => o.AutoServiceId == autoServiceId);
        return _mapper.Map<IEnumerable<RepairOrderDto>>(orders);
    }

    public async Task<IEnumerable<RepairOrderDto>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<ServiceStatus>(status, out var serviceStatus))
            throw new AppException(ExceptionType.BadRequest, "InvalidServiceStatus");

        var orders = await _repairOrderRepository.FindAsync(o => o.ServiceStatus == serviceStatus);
        return _mapper.Map<IEnumerable<RepairOrderDto>>(orders);
    }
} 