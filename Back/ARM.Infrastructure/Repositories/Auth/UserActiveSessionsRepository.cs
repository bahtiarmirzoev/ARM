using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Auth;
using ARM.Core.Entities.Auth;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Auth;

public class UserActiveSessionsRepository : IUserActiveSessionsRepository
{
    private readonly ARMContext _context;

    public UserActiveSessionsRepository(ARMContext context)
        => _context = context;
    
    public async Task<UserActiveSessionsEntity> GetByIdAsync(string id)
        => await _context.UserActiveSessions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false)
        ?? throw new AppException(ExceptionType.NotFound, "UserDeviceTokenNotFound"); 

    public async Task<IEnumerable<UserActiveSessionsEntity>> GetAllAsync()
    {
        var userDeviceTokens = await _context.UserActiveSessions
            .AsNoTracking()
            .ToListAsync()
            .ConfigureAwait(false);
        
        return userDeviceTokens.Any() ? userDeviceTokens : throw new AppException(ExceptionType.NotFound, "NoUserDeviceTokensFound");
    }

    public async Task AddAsync(UserActiveSessionsEntity userActiveSessionsEntity)
        => await _context.UserActiveSessions.AddAsync(userActiveSessionsEntity);

    public async Task UpdateAsync(IEnumerable<UserActiveSessionsEntity> userDeviceTokens)
    {
        foreach (var userDeviceToken in userDeviceTokens)
        {
            var updatedCount = await _context.UserActiveSessions
                .Where(u => u.Id == userDeviceToken.Id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.UserId, userDeviceToken.UserId)
                    .SetProperty(u => u.AccessToken, userDeviceToken.AccessToken)
                    .SetProperty(u => u.RefreshToken, userDeviceToken.RefreshToken)
                    .SetProperty(u => u.RefreshTokenExpiryTime, userDeviceToken.RefreshTokenExpiryTime)
                    .SetProperty(u => u.DeviceInfo, userDeviceToken.DeviceInfo));
            
            if (updatedCount == 0)  throw new AppException(ExceptionType.NotFound, "UserDeviceTokenNotFound"); 
        }
    }

    public async Task DeleteAsync(string id)
    {
        var item = await _context.UserActiveSessions
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        if (item == 0) throw new AppException(ExceptionType.NotFound, "UserDeviceTokenNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<UserActiveSessionsEntity, bool>> predicate)
        => await _context.UserActiveSessions.AnyAsync(predicate);

    public async Task<ICollection<UserActiveSessionsEntity>> FindAsync(Expression<Func<UserActiveSessionsEntity, bool>> predicate)
        => await _context.UserActiveSessions
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}