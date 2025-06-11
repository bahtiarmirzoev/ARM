using ARM.Core.Abstractions.Repositories.Auth;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Auth;
using AutoMapper;

namespace ARM.Application.Services.Auth;

public class UserActiveSessionsService(
    IMapper mapper,
    IUserActiveSessionsRepository userActiveSessionsRepository,
    IUnitOfWork unitOfWork)
    : IUserActiveSessionsService
{
    public async Task<UserActiveSessionsEntity> AddUserActiveSessionAsync(CreateUserActiveSessionDto token)
    {
        var userDeviceToken = mapper.Map<UserActiveSessionsEntity>(token);
        await unitOfWork.BeginTransactionAsync();
        try
        {
            await userActiveSessionsRepository.AddAsync(userDeviceToken);
            await unitOfWork.CommitTransactionAsync();
            return userDeviceToken;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IEnumerable<UserActiveSessionsEntity>> GetUserActiveSessionAsync(string userId)
    {
        return await userActiveSessionsRepository.FindAsync(t => t.UserId == userId);
    }
    
    public async Task<UserActiveSessionsEntity> UpdateUserActiveSessionAsync(string id, UpdateUserActiveSessionDto tokenDto)
    {
        var userDeviceToken = await userActiveSessionsRepository.GetByIdAsync(id);

        mapper.Map(tokenDto, userDeviceToken);

        await unitOfWork.BeginTransactionAsync();
        try
        {
            await userActiveSessionsRepository.UpdateAsync(new[]{userDeviceToken});
            await unitOfWork.CommitTransactionAsync();
            return userDeviceToken;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    public async Task<UserActiveSessionsEntity> DeleteUserActiveSessionAsync(string tokenId)
    {
        var userDeviceToken = await userActiveSessionsRepository.GetByIdAsync(tokenId);

        await unitOfWork.BeginTransactionAsync();
        try
        {
            await userActiveSessionsRepository.DeleteAsync(tokenId);
            await unitOfWork.CommitTransactionAsync();
            return userDeviceToken;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> IsUserSessionActiveAsync(string userId)
    {
        var activeSessions = await userActiveSessionsRepository.FindAsync(u => u.UserId == userId );
        return activeSessions.Any();
    }
}