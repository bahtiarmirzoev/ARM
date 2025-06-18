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
using System;

namespace ARM.Application.Services.Main;

public class WorkingHourService : IWorkingHourService
{
    private readonly IMapper _mapper;
    private readonly IWorkingHourRepository _workingHourRepository;
    private readonly IValidator<CreateWorkingHourDto> _createWorkingHourValidator;
    private readonly IValidator<UpdateWorkingHourDto> _updateWorkingHourValidator;
    private readonly IUnitOfWork _unitOfWork;

    public WorkingHourService(
        IMapper mapper,
        IWorkingHourRepository workingHourRepository,
        IValidator<CreateWorkingHourDto> createWorkingHourValidator,
        IValidator<UpdateWorkingHourDto> updateWorkingHourValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _workingHourRepository = workingHourRepository;
        _createWorkingHourValidator = createWorkingHourValidator;
        _updateWorkingHourValidator = updateWorkingHourValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkingHourDto> CreateAsync(CreateWorkingHourDto dto)
    {
        await _createWorkingHourValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var workingHourEntity = _mapper.Map<WorkingHourEntity>(dto);
            workingHourEntity.Day = Enum.Parse<DayOfWeek>(dto.DayOfWeek.ToString());
            workingHourEntity.OpenTime = dto.OpenTime;
            workingHourEntity.CloseTime = dto.CloseTime;
            await _workingHourRepository.AddAsync(workingHourEntity);

            return _mapper.Map<WorkingHourDto>(workingHourEntity);
        });
    }

    public async Task<WorkingHourDto> UpdateAsync(string id, UpdateWorkingHourDto dto)
    {
        var existingWorkingHour = await _workingHourRepository.GetByIdAsync(id);
        await _updateWorkingHourValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            existingWorkingHour.Day = Enum.Parse<DayOfWeek>(dto.DayOfWeek.ToString());
            existingWorkingHour.OpenTime = dto.OpenTime;
            existingWorkingHour.CloseTime = dto.CloseTime;
            await _workingHourRepository.UpdateAsync([existingWorkingHour]);

            return _mapper.Map<WorkingHourDto>(existingWorkingHour);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _workingHourRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<WorkingHourDto> GetByIdAsync(string id)
    {
        var workingHour = await _workingHourRepository.GetByIdAsync(id);
        return _mapper.Map<WorkingHourDto>(workingHour);
    }

    public async Task<IEnumerable<WorkingHourDto>> GetAllAsync()
    {
        var workingHours = await _workingHourRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<WorkingHourDto>>(workingHours);
    }

    public async Task<IEnumerable<WorkingHourDto>> GetByAutoServiceIdAsync(string autoServiceId)
    {
        var workingHours = await _workingHourRepository.FindAsync(wh => wh.AutoServiceId == autoServiceId);
        return _mapper.Map<IEnumerable<WorkingHourDto>>(workingHours);
    }

    public async Task<IEnumerable<WorkingHourDto>> GetByDayOfWeekAsync(string dayOfWeek)
    {
        if (!Enum.TryParse<DayOfWeek>(dayOfWeek, out var day))
            throw new AppException(ExceptionType.BadRequest, "InvalidDayOfWeek");

        var workingHours = await _workingHourRepository.FindAsync(wh => wh.Day == day);
        return _mapper.Map<IEnumerable<WorkingHourDto>>(workingHours);
    }
}