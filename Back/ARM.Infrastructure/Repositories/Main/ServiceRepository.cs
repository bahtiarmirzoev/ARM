using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class ServiceRepository : IServiceRepository
{
    private readonly ARMContext _context;

    public ServiceRepository(ARMContext context)
        => _context = context;

    public async Task<ServiceEntity> GetByIdAsync(string id)
        => await _context.Services
               .Include(s => s.Venues)
               .AsNoTracking()
               .FirstOrDefaultAsync(s => s.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "ServiceNotFound");

    public async Task<IEnumerable<ServiceEntity>> GetAllAsync()
    {
        var services = await _context.Services
            .Include(s => s.Venues)
            .AsNoTracking()
            .ToListAsync();

        return services;
    }

    public async Task AddAsync(ServiceEntity entity)
        => await _context.Services.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<ServiceEntity> entities)
    {
        foreach (var service in entities)
        {
            var result = await _context.Services
                .Where(s => s.Id == service.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Name, service.Name)
                    .SetProperty(s => s.Description, service.Description)
                    .SetProperty(s => s.Price, service.Price)
                    .SetProperty(s => s.Duration, service.Duration)
                    .SetProperty(s => s.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "ServiceNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Services
            .Include(s => s.Venues)
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "ServiceNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<ServiceEntity, bool>> predicate)
        => await _context.Services.AnyAsync(predicate);

    public async Task<ICollection<ServiceEntity>> FindAsync(Expression<Func<ServiceEntity, bool>> predicate)
        => await _context.Services
            .Include(s => s.Venues)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
