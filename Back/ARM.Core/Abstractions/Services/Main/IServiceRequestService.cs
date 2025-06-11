using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface IServiceRequestService
{
    Task<ServiceRequestDto> CreateAsync(CreateServiceRequestDto dto);
    Task<ServiceRequestDto> UpdateAsync(string id, UpdateServiceRequestDto dto);
    Task<bool> DeleteAsync(string id);
    Task<ServiceRequestDto> GetByIdAsync(string id);
    Task<IEnumerable<ServiceRequestDto>> GetAllAsync();
    Task<IEnumerable<ServiceRequestDto>> GetByAutoServiceIdAsync(string autoServiceId);
    Task<IEnumerable<ServiceRequestDto>> GetByStatusAsync(string status);
} 