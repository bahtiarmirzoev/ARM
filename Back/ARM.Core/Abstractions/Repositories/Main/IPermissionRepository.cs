using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IPermissionRepository
{
    Task<PermissionEntity> GetByIdAsync(string id);
    Task<IEnumerable<PermissionEntity>> GetAllAsync();
    Task AddAsync(PermissionEntity entity);
    Task UpdateAsync(IEnumerable<PermissionEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<PermissionEntity, bool>> predicate);
    Task<ICollection<PermissionEntity>> FindAsync(Expression<Func<PermissionEntity, bool>> predicate);
}