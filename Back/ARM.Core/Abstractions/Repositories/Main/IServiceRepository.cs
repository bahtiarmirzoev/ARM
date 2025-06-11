using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IServiceRepository
{
    Task<ServiceEntity> GetByIdAsync(string id);
    Task<IEnumerable<ServiceEntity>> GetAllAsync();
    Task AddAsync(ServiceEntity entity);
    Task UpdateAsync(IEnumerable<ServiceEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<ServiceEntity, bool>> predicate);
    Task<ICollection<ServiceEntity>> FindAsync(Expression<Func<ServiceEntity, bool>> predicate);
}