using System.Collections.Generic;
using System.Threading.Tasks;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;

namespace ARM.Core.Abstractions.Services.Main;

public interface ICustomerService
{
    Task<CustomerDto> CreateAsync(CreateCustomerDto dto);
    Task<CustomerDto> UpdateAsync(string id, UpdateCustomerDto dto);
    Task DeleteAsync(string id);
    Task<CustomerDto> GetByIdAsync(string id);
    Task<IEnumerable<CustomerDto>> GetAllAsync();
}