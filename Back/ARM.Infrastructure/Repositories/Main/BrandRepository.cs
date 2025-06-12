using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class BrandRepository : IBrandRepository
{
    private readonly ARMContext _context;

    public BrandRepository(ARMContext context)
            => _context = context;
    
    public async Task<BrandEntity> GetByIdAsync(string id)
        => await _context.Brands
               .Include(ar => ar.Venues)
               .ThenInclude(v => v.Services)
               .Include(ar => ar.Reviews)
               .Include(ar => ar.WorkingHours)
               .AsNoTracking()
               .FirstOrDefaultAsync(ar => ar.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "BrandNotFound"); 

    public async Task<IEnumerable<BrandEntity>> GetAllAsync()
    {
        var brands = await _context.Brands
            .Include(ar => ar.Venues)
            .ThenInclude(v => v.Services)
            .Include(ar => ar.Reviews)
            .Include(ar => ar.WorkingHours)
            .AsNoTracking()
            .ToListAsync();

        return brands;
    }

    public async Task AddAsync(BrandEntity entity)
        => await _context.Brands.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<BrandEntity> entity)
    {
        foreach (var brand in entity)
        {
            var result = await _context.Brands
                .Where(ar => ar.Id == brand.Id)
                .ExecuteUpdateAsync(ar => ar
                    .SetProperty(ar => ar.Name, brand.Name)
                    .SetProperty(ar => ar.Description, brand.Description)
                    .SetProperty(ar => ar.PhoneNumber, brand.PhoneNumber)
                    .SetProperty(ar => ar.Email, brand.Email)
                    .SetProperty(ar => ar.Address, brand.Address)
                    .SetProperty(ar => ar.MaxCarsPerDay, brand.MaxCarsPerDay)
                    .SetProperty(ar => ar.HasParking, brand.HasParking)
                    .SetProperty(ar => ar.HasWaitingRoom, brand.HasWaitingRoom)
                    .SetProperty(ar => ar.Rating, brand.Rating)
                    .SetProperty(ar => ar.UpdatedAt, DateTime.UtcNow));

            if (result == 0) 
                throw new AppException(ExceptionType.NotFound, "BrandNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Brands
            .Include(ar => ar.Venues)
            .ThenInclude(v => v.Services)
            .Include(ar => ar.Reviews)
            .Include(ar => ar.WorkingHours)
            .Where(ar => ar.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0) 
            throw new AppException(ExceptionType.NotFound,"BrandNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<BrandEntity, bool>> predicate)
        => await _context.Brands.AnyAsync(predicate);

    public async Task<ICollection<BrandEntity>> FindAsync(Expression<Func<BrandEntity, bool>> predicate)
        => await _context.Brands
            .Include(ar => ar.Venues)
            .ThenInclude(v => v.Services)
            .Include(ar => ar.Reviews)
            .Include(ar => ar.WorkingHours)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}