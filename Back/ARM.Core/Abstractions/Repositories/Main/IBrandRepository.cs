using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IBrandRepository
{
    Task<BrandEntity> GetByIdAsync(string id);
    Task<IEnumerable<BrandEntity>> GetAllAsync();
    Task AddAsync(BrandEntity entity);
    Task UpdateAsync(IEnumerable<BrandEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<BrandEntity, bool>> predicate);
    Task<ICollection<BrandEntity>> FindAsync(Expression<Func<BrandEntity, bool>> predicate);
}