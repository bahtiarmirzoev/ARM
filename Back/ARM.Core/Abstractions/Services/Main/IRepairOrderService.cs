using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface IRepairOrderService
{
    Task<RepairOrderDto> CreateAsync(CreateRepairOrderDto dto);
    Task<RepairOrderDto> UpdateAsync(string id, UpdateRepairOrderDto dto);
    Task<bool> DeleteAsync(string id);
    Task<RepairOrderDto> GetByIdAsync(string id);
    Task<IEnumerable<RepairOrderDto>> GetAllAsync();
    Task<IEnumerable<RepairOrderDto>> GetByAutoServiceIdAsync(string autoServiceId);
    Task<IEnumerable<RepairOrderDto>> GetByStatusAsync(string status);
} 