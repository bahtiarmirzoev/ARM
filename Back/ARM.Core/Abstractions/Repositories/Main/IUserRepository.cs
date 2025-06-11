using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IUserRepository
{
    Task<UserEntity> GetByIdAsync(string id);
    Task<IEnumerable<UserEntity>> GetAllAsync();
    Task AddAsync(UserEntity entity);
    Task UpdateAsync(IEnumerable<UserEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<UserEntity, bool>> predicate);
    Task<ICollection<UserEntity>> FindAsync(Expression<Func<UserEntity, bool>> predicate);
}