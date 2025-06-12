using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class VenueRepository : IVenueRepository
{
    private readonly ARMContext _context;

    public VenueRepository(ARMContext context)
        => _context = context;

    public async Task<VenueEntity> GetByIdAsync(string id)
        => await _context.Venues
               .Include(v => v.Brand)
               .Include(v => v.Services)
               .AsNoTracking()
               .FirstOrDefaultAsync(v => v.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "VenueNotFound");

    public async Task<IEnumerable<VenueEntity>> GetAllAsync()
    {
        var venues = await _context.Venues
            .Include(v => v.Services)
            .Include(v => v.Brand)
            .AsNoTracking()
            .ToListAsync();

        return venues.Any() ? venues : throw new AppException(ExceptionType.NotFound, "NoVenuesFound");
    }

    public async Task AddAsync(VenueEntity entity)
        => await _context.Venues.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<VenueEntity> entities)
    {
        foreach (var venue in entities)
        {
            var result = await _context.Venues
                .Where(v => v.Id == venue.Id)
                .ExecuteUpdateAsync(v => v
                    .SetProperty(v => v.Name, venue.Name)
                    .SetProperty(v => v.Address, venue.Address)
                    .SetProperty(v => v.PhoneNumber, venue.PhoneNumber)
                    .SetProperty(v => v.Email, venue.Email)
                    .SetProperty(v => v.IsOpen, venue.IsOpen)
                    .SetProperty(v => v.Latitude, venue.Latitude)
                    .SetProperty(v => v.Longitude, venue.Longitude)
                    .SetProperty(v => v.BrandId, venue.BrandId)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "VenueNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Venues
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "VenueNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<VenueEntity, bool>> predicate)
        => await _context.Venues.AnyAsync(predicate);

    public async Task<ICollection<VenueEntity>> FindAsync(Expression<Func<VenueEntity, bool>> predicate)
        => await _context.Venues
            .Include(v => v.Services)
            .Include(v => v.Brand)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}