using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface IWorkingHourService
{
    Task<WorkingHourDto> CreateAsync(CreateWorkingHourDto dto);
    Task<WorkingHourDto> UpdateAsync(string id, UpdateWorkingHourDto dto);
    Task<bool> DeleteAsync(string id);
    Task<WorkingHourDto> GetByIdAsync(string id);
    Task<IEnumerable<WorkingHourDto>> GetAllAsync();
    Task<IEnumerable<WorkingHourDto>> GetByAutoServiceIdAsync(string autoServiceId);
    Task<IEnumerable<WorkingHourDto>> GetByDayOfWeekAsync(string dayOfWeek);
} 