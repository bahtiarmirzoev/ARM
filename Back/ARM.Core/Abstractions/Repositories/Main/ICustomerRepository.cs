using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Repositories.Main;

public interface ICustomerRepository
{
    Task<CustomerEntity> GetByIdAsync(string id);
    Task<IEnumerable<CustomerEntity>> GetAllAsync();
    Task AddAsync(CustomerEntity entity);
    Task UpdateAsync(IEnumerable<CustomerEntity> entities);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<CustomerEntity, bool>> predicate);
    Task<ICollection<CustomerEntity>> FindAsync(Expression<Func<CustomerEntity, bool>> predicate);
}