using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class RoleRepository : IRoleRepository
{
    private readonly ARMContext _context;

    public RoleRepository(ARMContext context)
        => _context = context;

    public async Task<RoleEntity> GetByIdAsync(string id)
        => await _context.Roles
               .Include(r => r.Permissions)
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "RoleNotFound");

    public async Task<IEnumerable<RoleEntity>> GetAllAsync()
    {
        var roles = await _context.Roles
            .Include(r => r.Permissions)
            .AsNoTracking()
            .ToListAsync();

        return roles.Any() ? roles : throw new AppException(ExceptionType.NotFound, "NoRolesFound");
    }

    public async Task AddAsync(RoleEntity entity)
        => await _context.Roles.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<RoleEntity> entities)
    {
        foreach (var role in entities)
        {
            var result = await _context.Roles
                .Where(r => r.Id == role.Id)
                .ExecuteUpdateAsync(r => r
                    .SetProperty(r => r.Name, role.Name)
                    .SetProperty(r => r.Description, role.Description)
                    .SetProperty(r => r.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "RoleNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Roles
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "RoleNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<RoleEntity, bool>> predicate)
        => await _context.Roles.AnyAsync(predicate);

    public async Task<ICollection<RoleEntity>> FindAsync(Expression<Func<RoleEntity, bool>> predicate)
        => await _context.Roles
            .Include(r => r.Permissions)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
