using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface ICarRepository
{
    Task<CarEntity> GetByIdAsync(string id);
    Task<IEnumerable<CarEntity>> GetAllAsync();
    Task AddAsync(CarEntity entity);
    Task UpdateAsync(IEnumerable<CarEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<CarEntity, bool>> predicate);
    Task<ICollection<CarEntity>> FindAsync(Expression<Func<CarEntity, bool>> predicate);
}