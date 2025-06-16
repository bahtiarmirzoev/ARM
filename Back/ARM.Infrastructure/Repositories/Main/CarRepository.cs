using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class CarRepository : ICarRepository
{
    private readonly ARMContext _context;

    public CarRepository(ARMContext context)
        => _context = context;

    public async Task<CarEntity> GetByIdAsync(string id)
        => await _context.Cars
               .Include(c => c.RepairHistory) 
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "CarNotFound");

    public async Task<IEnumerable<CarEntity>> GetAllAsync()
    {
        var cars = await _context.Cars
            .Include(c => c.RepairHistory)
            .AsNoTracking()
            .ToListAsync();

        return cars;
    }

    public async Task AddAsync(CarEntity entity)
        => await _context.Cars.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<CarEntity> entity)
    {
        foreach (var car in entity)
        {
            var result = await _context.Cars
                .Where(c => c.Id == car.Id)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(c => c.Make, car.Make)
                    .SetProperty(c => c.Model, car.Model)
                    .SetProperty(c => c.CarPlate, car.CarPlate)
                    .SetProperty(c => c.Year, car.Year)
                    .SetProperty(c => c.Color, car.Color)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "CarNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Cars
            .Include(c => c.RepairHistory)
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "CarNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<CarEntity, bool>> predicate)
        => await _context.Cars.AnyAsync(predicate);

    public async Task<ICollection<CarEntity>> FindAsync(Expression<Func<CarEntity, bool>> predicate)
        => await _context.Cars
            .Include(c => c.RepairHistory)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
