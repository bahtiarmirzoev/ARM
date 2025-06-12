using System.Linq.Expressions;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IVenueRepository
{
    Task<VenueEntity> GetByIdAsync(string id);
    Task<IEnumerable<VenueEntity>> GetAllAsync();
    Task AddAsync(VenueEntity entity);
    Task UpdateAsync(IEnumerable<VenueEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<VenueEntity, bool>> predicate);
    Task<ICollection<VenueEntity>> FindAsync(Expression<Func<VenueEntity, bool>> predicate);
}