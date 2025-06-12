using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class ReviewRepository : IReviewRepository
{
    private readonly ARMContext _context;

    public ReviewRepository(ARMContext context)
        => _context = context;

    public async Task<ReviewEntity> GetByIdAsync(string id)
        => await _context.Reviews
               .Include(r => r.Customer)
               .Include(r => r.Brand)
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "ReviewNotFound");

    public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
    {
        var reviews = await _context.Reviews
            .Include(r => r.Customer)
            .Include(r => r.Brand)
            .AsNoTracking()
            .ToListAsync();

        return reviews.Any() ? reviews : throw new AppException(ExceptionType.NotFound, "NoReviewsFound");
    }

    public async Task AddAsync(ReviewEntity entity)
        => await _context.Reviews.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<ReviewEntity> entities)
    {
        foreach (var review in entities)
        {
            var result = await _context.Reviews
                .Where(r => r.Id == review.Id)
                .ExecuteUpdateAsync(r => r
                    .SetProperty(r => r.Rating, review.Rating)
                    .SetProperty(r => r.Comment, review.Comment)
                    .SetProperty(r => r.CustomerId, review.CustomerId)
                    .SetProperty(r => r.AutoServiceId, review.AutoServiceId)
                    .SetProperty(r => r.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "ReviewNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Reviews
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "ReviewNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<ReviewEntity, bool>> predicate)
        => await _context.Reviews.AnyAsync(predicate);

    public async Task<ICollection<ReviewEntity>> FindAsync(Expression<Func<ReviewEntity, bool>> predicate)
        => await _context.Reviews
            .Include(r => r.Customer)
            .Include(r => r.Brand)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
