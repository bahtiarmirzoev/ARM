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

    private HttpContext context => _httpContextAccessor.HttpContext;

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

    public async Task<RepairOrderDto> CreateAsync(CreateRepairOrderDto dto)
    {
        var customerId = context.GetCustomerId();
        await _createRepairOrderValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var repairOrderEntity = _mapper.Map<RepairOrderEntity>(dto);
            repairOrderEntity.CustomerId = customerId;
            repairOrderEntity.OrderDate = DateTime.UtcNow;
            repairOrderEntity.ServiceStatus = ServiceStatus.NotDone;

            await _repairOrderRepository.AddAsync(repairOrderEntity);

            return _mapper.Map<RepairOrderDto>(repairOrderEntity);
        });
    }

    public async Task<RepairOrderDto> UpdateAsync(string id, UpdateRepairOrderDto dto)
    {
        var customerId = context.GetCustomerId();
        var existingOrder = (await _repairOrderRepository.FindAsync(
                o => o.Id == id && o.CustomerId == customerId))
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
        var customerId = context.GetCustomerId();
        var order = (await _repairOrderRepository.FindAsync(
                o => o.Id == id && o.CustomerId == customerId))
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
        var customerId = context.GetCustomerId();
        var order = await _repairOrderRepository.FindAsync(o => o.Id == id && o.CustomerId == customerId)
            .EnsureFound("RepairOrderNotFound");
        return _mapper.Map<RepairOrderDto>(order);
    }

    public async Task<IEnumerable<RepairOrderDto>> GetAllAsync()
    {
        var customerId = context.GetCustomerId();
        var orders = await _repairOrderRepository.FindAsync(u => u.CustomerId == customerId);
        return _mapper.Map<IEnumerable<RepairOrderDto>>(orders);
    }

    public async Task<IEnumerable<RepairOrderDto>> GetByAutoServiceIdAsync(string autoServiceId)
    {
        var customerId = context.GetCustomerId();
        var orders = await _repairOrderRepository.FindAsync(o => o.AutoServiceId == autoServiceId && o.CustomerId == customerId);
        return _mapper.Map<IEnumerable<RepairOrderDto>>(orders);
    }

    public async Task<IEnumerable<RepairOrderDto>> GetByStatusAsync(string status)
    {
        var customerId = context.GetCustomerId();
        if (!Enum.TryParse<ServiceStatus>(status, out var serviceStatus))
            throw new AppException(ExceptionType.BadRequest, "InvalidServiceStatus");

        var orders = await _repairOrderRepository.FindAsync(o => o.ServiceStatus == serviceStatus && o.CustomerId == customerId);
        return _mapper.Map<IEnumerable<RepairOrderDto>>(orders);
    }
}