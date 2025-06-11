using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using AutoMapper;
using FluentValidation;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using ARM.Core.Enums;

namespace ARM.Application.Services.Main;

public class ServiceRequestService : IServiceRequestService
{
    private readonly IMapper _mapper;
    private readonly IServiceRequestRepository _serviceRequestRepository;
    private readonly IValidator<CreateServiceRequestDto> _createServiceRequestValidator;
    private readonly IValidator<UpdateServiceRequestDto> _updateServiceRequestValidator;
    private readonly IUnitOfWork _unitOfWork;

    public ServiceRequestService(
        IMapper mapper,
        IServiceRequestRepository serviceRequestRepository,
        IValidator<CreateServiceRequestDto> createServiceRequestValidator,
        IValidator<UpdateServiceRequestDto> updateServiceRequestValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _serviceRequestRepository = serviceRequestRepository;
        _createServiceRequestValidator = createServiceRequestValidator;
        _updateServiceRequestValidator = updateServiceRequestValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceRequestDto> CreateAsync(CreateServiceRequestDto dto)
    {
        await _createServiceRequestValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var serviceRequestEntity = _mapper.Map<ServiceRequestEntity>(dto);
            serviceRequestEntity.RequestDate = DateTime.UtcNow;
            await _serviceRequestRepository.AddAsync(serviceRequestEntity);
            
            return _mapper.Map<ServiceRequestDto>(serviceRequestEntity);
        });
    }

    public async Task<ServiceRequestDto> UpdateAsync(string id, UpdateServiceRequestDto dto)
    {
        var existingRequest = await _serviceRequestRepository.GetByIdAsync(id);
        await _updateServiceRequestValidator.ValidateAndThrowAsync(dto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingRequest);
            existingRequest.UpdatedAt = DateTime.UtcNow;
            await _serviceRequestRepository.UpdateAsync(new[] { existingRequest });
            
            return _mapper.Map<ServiceRequestDto>(existingRequest);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _serviceRequestRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<ServiceRequestDto> GetByIdAsync(string id)
    {
        var request = await _serviceRequestRepository.GetByIdAsync(id);
        return _mapper.Map<ServiceRequestDto>(request);
    }

    public async Task<IEnumerable<ServiceRequestDto>> GetAllAsync()
    {
        var requests = await _serviceRequestRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ServiceRequestDto>>(requests);
    }

    public async Task<IEnumerable<ServiceRequestDto>> GetByUserIdAsync(string userId)
    {
        var requests = await _serviceRequestRepository.FindAsync(r => r.Name == userId);
        return _mapper.Map<IEnumerable<ServiceRequestDto>>(requests);
    }

    public async Task<IEnumerable<ServiceRequestDto>> GetByAutoServiceIdAsync(string autoServiceId)
    {
        var requests = await _serviceRequestRepository.FindAsync(r => r.AutoRepairId == autoServiceId);
        return _mapper.Map<IEnumerable<ServiceRequestDto>>(requests);
    }

    public async Task<IEnumerable<ServiceRequestDto>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<RequestStatus>(status, out var requestStatus))
            throw new AppException(ExceptionType.BadRequest, "InvalidStatus");

        var requests = await _serviceRequestRepository.FindAsync(r => r.Status == requestStatus);
        return _mapper.Map<IEnumerable<ServiceRequestDto>>(requests);
    }
} 