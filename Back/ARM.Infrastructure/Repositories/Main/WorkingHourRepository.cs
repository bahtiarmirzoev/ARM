using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class WorkingHourRepository : IWorkingHourRepository
{
    private readonly ARMContext _context;

    public WorkingHourRepository(ARMContext context)
        => _context = context;

    public async Task<WorkingHourEntity> GetByIdAsync(string id)
        => await _context.WorkingHours
               .Include(wh => wh.Brand)
               .AsNoTracking()
               .FirstOrDefaultAsync(wh => wh.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "WorkingHourNotFound");

    public async Task<IEnumerable<WorkingHourEntity>> GetAllAsync()
    {
        var workingHours = await _context.WorkingHours
            .Include(wh => wh.Brand)
            .AsNoTracking()
            .ToListAsync();

        return workingHours;
    }

    public async Task AddAsync(WorkingHourEntity entity)
        => await _context.WorkingHours.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<WorkingHourEntity> entities)
    {
        foreach (var workingHour in entities)
        {
            var result = await _context.WorkingHours
                .Where(wh => wh.Id == workingHour.Id)
                .ExecuteUpdateAsync(wh => wh
                    .SetProperty(wh => wh.Day, workingHour.Day)
                    .SetProperty(wh => wh.OpenTime, workingHour.OpenTime)
                    .SetProperty(wh => wh.CloseTime, workingHour.CloseTime));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "WorkingHourNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.WorkingHours
            .Where(wh => wh.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "WorkingHourNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<WorkingHourEntity, bool>> predicate)
        => await _context.WorkingHours.AnyAsync(predicate);

    public async Task<ICollection<WorkingHourEntity>> FindAsync(Expression<Func<WorkingHourEntity, bool>> predicate)
        => await _context.WorkingHours
            .Include(wh => wh.Brand)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
} 