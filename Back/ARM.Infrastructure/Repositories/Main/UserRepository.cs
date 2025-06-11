using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class UserRepository : IUserRepository
{
    private readonly ARMContext _context;

    public UserRepository(ARMContext context)
        => _context = context;

    public async Task<UserEntity> GetByIdAsync(string id)
        => await _context.Users
               .Include(u => u.Role) 
               .Include(u => u.Cars) 
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "UserNotFound");

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var users = await _context.Users
            .Include(u => u.Role)
            .Include(u => u.Cars)
            .AsNoTracking()
            .ToListAsync();

        return users.Any() ? users : throw new AppException(ExceptionType.NotFound, "NoUsersFound");
    }

    public async Task AddAsync(UserEntity entity)
        => await _context.Users.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<UserEntity> entities)
    {
        foreach (var user in entities)
        {
            var result = await _context.Users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.Name, user.Name)
                    .SetProperty(u => u.Surname, user.Surname)
                    .SetProperty(u => u.Email, user.Email)
                    .SetProperty(u => u.Password, user.Password) 
                    .SetProperty(u => u.EmailVerified, user.EmailVerified)
                    .SetProperty(u => u.RoleId, user.RoleId)
                    .SetProperty(u => u.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "UserNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Users
            .Include(u => u.Role)
            .Include(u => u.Cars)
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "UserNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<UserEntity, bool>> predicate)
        => await _context.Users.AnyAsync(predicate);

    public async Task<ICollection<UserEntity>> FindAsync(Expression<Func<UserEntity, bool>> predicate)
        => await _context.Users
            .Include(u => u.Role)
            .Include(u => u.Cars)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
