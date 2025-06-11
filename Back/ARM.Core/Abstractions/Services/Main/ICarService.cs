using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface ICarService
{
    Task<CarDto> CreateAsync(CreateCarDto dto);
    Task<CarDto> UpdateAsync(string id, UpdateCarDto dto);
    Task<bool> DeleteAsync(string id);
    Task<CarDto> GetByIdAsync(string id);
    Task<IEnumerable<CarDto>> GetAllAsync();
} 