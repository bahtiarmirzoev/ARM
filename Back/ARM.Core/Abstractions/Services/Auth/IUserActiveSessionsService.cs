using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Auth;

namespace ARM.Core.Abstractions.Services.Auth;

public interface IUserActiveSessionsService
{
    Task<UserActiveSessionsEntity> AddUserActiveSessionAsync(CreateUserActiveSessionDto token);
    Task<IEnumerable<UserActiveSessionsEntity>> GetUserActiveSessionAsync(string userId);
    Task<UserActiveSessionsEntity> UpdateUserActiveSessionAsync(string id, UpdateUserActiveSessionDto token);
    Task<UserActiveSessionsEntity> DeleteUserActiveSessionAsync(string tokenId);
    Task<bool> IsUserSessionActiveAsync(string userId);
}