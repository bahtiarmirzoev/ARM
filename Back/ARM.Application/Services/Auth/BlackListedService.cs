using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Auth;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Auth;
using AutoMapper;

namespace ARM.Application.Services.Auth;

public class BlackListedService(IMapper mapper, IBlackListedRepository blackListedRepository, IUnitOfWork unitOfWork)
    : IBlackListedService
{
    public async Task<IEnumerable<BlackListedDto>> GetAllBlackListedAsync()
    {
        var blackListedItems = await blackListedRepository.GetAllAsync();
        return mapper.Map<IEnumerable<BlackListedDto>>(blackListedItems);
    }

    public async Task<BlackListedDto> GetBlackListedByIdAsync(int id)
    {
        var blackListedItem = await blackListedRepository.GetByIdAsync(id);
        return mapper.Map<BlackListedDto>(blackListedItem);
    }

    public async Task<BlackListedDto> AddToBlackListAsync(CreateBlackListedDto createBlackListedDto)
    {
        var existingItem = await blackListedRepository.AnyAsync(b => b.AccessToken == createBlackListedDto.AccessToken
        && b.RefreshToken == createBlackListedDto.RefreshToken);
        if (existingItem)
            throw new AppException(ExceptionType.CredentialsAlreadyExists, "TokenAlreadyBlacklisted");
        
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var blackListed = mapper.Map<BlackListedEntity>(createBlackListedDto);
            await blackListedRepository.AddAsync(blackListed);
            await unitOfWork.CommitTransactionAsync();
            
            return mapper.Map<BlackListedDto>(blackListed);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<BlackListedDto> UpdateBlackListAsync(UpdateBlackListedDto updateBlackListedDto)
    {
        var existingBlackListed = await blackListedRepository.GetByIdAsync(updateBlackListedDto.Id);
        
        await unitOfWork.BeginTransactionAsync();

        try
        {
            mapper.Map(updateBlackListedDto, existingBlackListed);
            await blackListedRepository.UpdateAsync(new[] {existingBlackListed});
            await unitOfWork.CommitTransactionAsync();
            
            return mapper.Map<BlackListedDto>(existingBlackListed);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> DeleteFromBlackListAsync(int id)
    {
        await blackListedRepository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> IsBlackListedAsync(BlackListedDto blackListedDto)
    {
        var isAccessTokenBlackListed = await blackListedRepository.AnyAsync(b => b.AccessToken == blackListedDto.AccessToken);
        var isRefreshTokenBlackListed = await blackListedRepository.AnyAsync(b => b.RefreshToken == blackListedDto.RefreshToken);
    
        return isAccessTokenBlackListed  || isRefreshTokenBlackListed ;
    }
}