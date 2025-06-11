using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;

namespace ARM.Core.Abstractions.Services.Auth;

public interface IBlackListedService
{
    Task<IEnumerable<BlackListedDto>> GetAllBlackListedAsync(); 
    Task<BlackListedDto> GetBlackListedByIdAsync(int id);
    Task<BlackListedDto> AddToBlackListAsync(CreateBlackListedDto createBlackListedDto);
    Task<BlackListedDto> UpdateBlackListAsync(UpdateBlackListedDto updateBlackListedDto);
    Task<bool> DeleteFromBlackListAsync(int id);
    Task<bool> IsBlackListedAsync(BlackListedDto blackListedDto);
}