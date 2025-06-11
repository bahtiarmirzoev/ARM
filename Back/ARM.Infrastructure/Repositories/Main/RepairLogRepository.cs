using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class RepairLogRepository : IRepairLogRepository
{
    private readonly ARMContext _context;

    public RepairLogRepository(ARMContext context)
        => _context = context;

    public async Task<RepairLogEntity> GetByIdAsync(string id)
        => await _context.RepairLogs
               .Include(r => r.Car) 
               .Include(r => r.Brand)
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "RepairLogNotFound");

    public async Task<IEnumerable<RepairLogEntity>> GetAllAsync()
    {
        var logs = await _context.RepairLogs
            .Include(r => r.Car)
            .Include(r => r.Brand)
            .AsNoTracking()
            .ToListAsync();

        return logs.Any() ? logs : throw new AppException(ExceptionType.NotFound, "NoRepairLogsFound");
    }

    public async Task AddAsync(RepairLogEntity entity)
        => await _context.RepairLogs.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<RepairLogEntity> entities)
    {
        foreach (var log in entities)
        {
            var result = await _context.RepairLogs
                .Where(r => r.Id == log.Id)
                .ExecuteUpdateAsync(r => r
                    .SetProperty(r => r.RepairDate, log.RepairDate)
                    .SetProperty(r => r.Description, log.Description)
                    .SetProperty(r => r.Diagnosis, log.Diagnosis)
                    .SetProperty(r => r.WorkPerformed, log.WorkPerformed)
                    .SetProperty(r => r.PartsReplaced, log.PartsReplaced)
                    .SetProperty(r => r.Recommendations, log.Recommendations)
                    .SetProperty(r => r.Cost, log.Cost)
                    .SetProperty(r => r.PartsCost, log.PartsCost)
                    .SetProperty(r => r.LaborCost, log.LaborCost)
                    .SetProperty(r => r.Mileage, log.Mileage)
                    .SetProperty(r => r.CarId, log.CarId)
                    .SetProperty(r => r.AutoServiceId, log.AutoServiceId)
                    .SetProperty(r => r.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "RepairLogNotFound");
        }
    }


    public async Task DeleteAsync(string id)
    {
        var result = await _context.RepairLogs
            .Include(r => r.Car)
            .Include(r => r.Brand)
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "RepairLogNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<RepairLogEntity, bool>> predicate)
        => await _context.RepairLogs.AnyAsync(predicate);

    public async Task<ICollection<RepairLogEntity>> FindAsync(Expression<Func<RepairLogEntity, bool>> predicate)
        => await _context.RepairLogs
            .Include(r => r.Car)
            .Include(r => r.Brand)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}