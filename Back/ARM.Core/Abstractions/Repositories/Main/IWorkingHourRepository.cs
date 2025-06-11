using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IWorkingHourRepository
{
    Task<WorkingHourEntity> GetByIdAsync(string id);
    Task<IEnumerable<WorkingHourEntity>> GetAllAsync();
    Task AddAsync(WorkingHourEntity entity);
    Task UpdateAsync(IEnumerable<WorkingHourEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<WorkingHourEntity, bool>> predicate);
    Task<ICollection<WorkingHourEntity>> FindAsync(Expression<Func<WorkingHourEntity, bool>> predicate);
} 