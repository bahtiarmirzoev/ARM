using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IRepairLogRepository
{
    Task<RepairLogEntity> GetByIdAsync(string id);
    Task<IEnumerable<RepairLogEntity>> GetAllAsync();
    Task AddAsync(RepairLogEntity entity);
    Task UpdateAsync(IEnumerable<RepairLogEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<RepairLogEntity, bool>> predicate);
    Task<ICollection<RepairLogEntity>> FindAsync(Expression<Func<RepairLogEntity, bool>> predicate);
}