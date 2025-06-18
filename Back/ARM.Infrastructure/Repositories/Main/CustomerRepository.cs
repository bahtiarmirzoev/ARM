using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class CustomerRepository : ICustomerRepository
{
    private readonly ARMContext _context;

    public CustomerRepository(ARMContext context)
        => _context = context;

    public async Task<CustomerEntity> GetByIdAsync(string id)
        => await _context.Customers
               .Include(c => c.Cars)
               .Include(c => c.RepairOrders)
               .Include(c => c.Reviews)
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "CustomerNotFound");

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        var customers = await _context.Customers
            .Include(c => c.Cars)
            .Include(c => c.RepairOrders)
            .Include(c => c.Reviews)
            .AsNoTracking()
            .ToListAsync();

        return customers;
    }

    public async Task AddAsync(CustomerEntity entity)
        => await _context.Customers.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<CustomerEntity> entities)
    {
        foreach (var customer in entities)
        {
            var result = await _context.Customers
                .Where(c => c.Id == customer.Id)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(c => c.Name, customer.Name)
                    .SetProperty(c => c.Surname, customer.Surname)
                    .SetProperty(c => c.Email, customer.Email)
                    .SetProperty(c => c.PhoneNumber, customer.PhoneNumber)
                    .SetProperty(c => c.Address, customer.Address)
                    .SetProperty(c => c.ProfilePicture, customer.ProfilePicture)
                    .SetProperty(c => c.EmailVerified, customer.EmailVerified)
                    .SetProperty(c => c.TwoFaEnabled, customer.TwoFaEnabled)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "CustomerNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Customers
            .Include(c => c.Cars)
            .Include(c => c.RepairOrders)
            .Include(c => c.Reviews)
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "CustomerNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<CustomerEntity, bool>> predicate)
        => await _context.Customers.AnyAsync(predicate);

    public async Task<ICollection<CustomerEntity>> FindAsync(Expression<Func<CustomerEntity, bool>> predicate)
        => await _context.Customers
            .Include(c => c.Cars)
            .Include(c => c.RepairOrders)
            .Include(c => c.Reviews)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}