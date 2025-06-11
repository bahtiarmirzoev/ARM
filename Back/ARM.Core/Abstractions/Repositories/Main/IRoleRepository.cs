using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IRoleRepository
{
    Task<RoleEntity> GetByIdAsync(string id);
    Task<IEnumerable<RoleEntity>> GetAllAsync();
    Task AddAsync(RoleEntity entity);
    Task UpdateAsync(IEnumerable<RoleEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<RoleEntity, bool>> predicate);
    Task<ICollection<RoleEntity>> FindAsync(Expression<Func<RoleEntity, bool>> predicate);
}