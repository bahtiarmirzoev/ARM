using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class ServiceRequestRepository : IServiceRequestRepository
{
    private readonly ARMContext _context;

    public ServiceRequestRepository(ARMContext context)
        => _context = context;

    public async Task<ServiceRequestEntity> GetByIdAsync(string id)
        => await _context.MakeOrderRequests
               .Include(m => m.Brand)
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "MakeOrderRequestNotFound");

    public async Task<IEnumerable<ServiceRequestEntity>> GetAllAsync()
    {
        var orders = await _context.MakeOrderRequests
            .Include(m => m.Brand)
            .AsNoTracking()
            .ToListAsync();

        return orders;
    }

    public async Task AddAsync(ServiceRequestEntity entity)
        => await _context.MakeOrderRequests.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<ServiceRequestEntity> entities)
    {
        foreach (var order in entities)
        {
            var result = await _context.MakeOrderRequests
                .Where(m => m.Id == order.Id)
                .ExecuteUpdateAsync(m => m
                    .SetProperty(m => m.Name, order.Name)
                    .SetProperty(m => m.Phone, order.Phone)
                    .SetProperty(m => m.TechnicalPassport, order.TechnicalPassport)
                    .SetProperty(m => m.Make, order.Make)
                    .SetProperty(m => m.Model, order.Model)
                    .SetProperty(m => m.ProblemDescription, order.ProblemDescription)
                    .SetProperty(m => m.RequestDate, order.RequestDate)
                    .SetProperty(m => m.IsProcessed, order.IsProcessed)
                    .SetProperty(m => m.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "MakeOrderRequestNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.MakeOrderRequests
            .Include(m => m.Brand)
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "MakeOrderRequestNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<ServiceRequestEntity, bool>> predicate)
        => await _context.MakeOrderRequests.AnyAsync(predicate);

    public async Task<ICollection<ServiceRequestEntity>> FindAsync(Expression<Func<ServiceRequestEntity, bool>> predicate)
        => await _context.MakeOrderRequests
            .Include(m => m.Brand)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}