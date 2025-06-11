using System.Linq.Expressions;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface IServiceRequestRepository
{
    Task<ServiceRequestEntity> GetByIdAsync(string id);
    Task<IEnumerable<ServiceRequestEntity>> GetAllAsync();
    Task AddAsync(ServiceRequestEntity entity);
    Task UpdateAsync(IEnumerable<ServiceRequestEntity> entity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<ServiceRequestEntity, bool>> predicate);
    Task<ICollection<ServiceRequestEntity>> FindAsync(Expression<Func<ServiceRequestEntity, bool>> predicate);
}