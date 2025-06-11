using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class RepairOrderRepository : IRepairOrderRepository
{
    private readonly ARMContext _context;

    public RepairOrderRepository(ARMContext context)
        => _context = context;

    public async Task<RepairOrderEntity> GetByIdAsync(string id)
        => await _context.RepairOrders
               .Include(ro => ro.User) 
               .Include(ro => ro.Car)
               .Include(ro => ro.Brand)
               .AsNoTracking()
               .FirstOrDefaultAsync(ro => ro.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "RepairOrderNotFound");

    public async Task<IEnumerable<RepairOrderEntity>> GetAllAsync()
    {
        var orders = await _context.RepairOrders
            .Include(ro => ro.User)
            .Include(ro => ro.Car)
            .Include(ro => ro.Brand)
            .AsNoTracking()
            .ToListAsync();

        return orders.Any() ? orders : throw new AppException(ExceptionType.NotFound, "NoRepairOrdersFound");
    }

    public async Task AddAsync(RepairOrderEntity entity)
        => await _context.RepairOrders.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<RepairOrderEntity> entities)
    {
        foreach (var order in entities)
        {
            var result = await _context.RepairOrders
                .Where(ro => ro.Id == order.Id)
                .ExecuteUpdateAsync(ro => ro
                    .SetProperty(ro => ro.UserId, order.UserId)
                    .SetProperty(ro => ro.CarId, order.CarId)
                    .SetProperty(ro => ro.AutoServiceId, order.AutoServiceId)
                    .SetProperty(ro => ro.ServiceTypeId, order.ServiceTypeId)
                    .SetProperty(ro => ro.ScheduledDate, order.ScheduledDate)
                    .SetProperty(ro => ro.EstimatedDuration, order.EstimatedDuration)
                    .SetProperty(ro => ro.DiagnosisResults, order.DiagnosisResults)
                    .SetProperty(ro => ro.ActualCost, order.ActualCost)
                    .SetProperty(ro => ro.CancellationReason, order.CancellationReason)
                    .SetProperty(ro => ro.OrderDate, order.OrderDate)
                    .SetProperty(ro => ro.ServiceStatus, order.ServiceStatus)
                    .SetProperty(ro => ro.EstimatedCost, order.EstimatedCost)
                    .SetProperty(ro => ro.CustomerComments, order.CustomerComments)
                    .SetProperty(ro => ro.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "RepairOrderNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.RepairOrders
            .Include(ro => ro.User)
            .Include(ro => ro.Car)
            .Include(ro => ro.Brand)
            .Where(ro => ro.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "RepairOrderNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<RepairOrderEntity, bool>> predicate)
        => await _context.RepairOrders.AnyAsync(predicate);

    public async Task<ICollection<RepairOrderEntity>> FindAsync(Expression<Func<RepairOrderEntity, bool>> predicate)
        => await _context.RepairOrders
            .Include(ro => ro.User)
            .Include(ro => ro.Car)
            .Include(ro => ro.Brand)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
