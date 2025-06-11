using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IReviewRepository
{
    Task<ReviewEntity> GetByIdAsync(string id);
    Task<IEnumerable<ReviewEntity>> GetAllAsync();
    Task AddAsync(ReviewEntity entity);
    Task UpdateAsync(IEnumerable<ReviewEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<ReviewEntity, bool>> predicate);
    Task<ICollection<ReviewEntity>> FindAsync(Expression<Func<ReviewEntity, bool>> predicate);
}