using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IRepairOrderRepository
{
    Task<RepairOrderEntity> GetByIdAsync(string id);
    Task<IEnumerable<RepairOrderEntity>> GetAllAsync();
    Task AddAsync(RepairOrderEntity entity);
    Task UpdateAsync(IEnumerable<RepairOrderEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<RepairOrderEntity, bool>> predicate);
    Task<ICollection<RepairOrderEntity>> FindAsync(Expression<Func<RepairOrderEntity, bool>> predicate);
}