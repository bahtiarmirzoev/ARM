using System.Linq.Expressions;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Repositories.Main;

public class PermissionRepository : IPermissionRepository
{
    private readonly ARMContext _context;

    public PermissionRepository(ARMContext context)
        => _context = context;

    public async Task<PermissionEntity> GetByIdAsync(string id)
        => await _context.Permissions
               .Include(p => p.Roles)
               .FirstOrDefaultAsync(p => p.Id == id)
           ?? throw new AppException(ExceptionType.NotFound, "PermissionNotFound");

    public async Task<IEnumerable<PermissionEntity>> GetAllAsync()
    {
        var permissions = await _context.Permissions
            .Include(p => p.Roles)
            .ToListAsync();

        return permissions;
    }

    public async Task AddAsync(PermissionEntity entity)
        => await _context.Permissions.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<PermissionEntity> entity)
    {
        foreach (var permission in entity)
        {
            var result = await _context.Permissions
                .Where(p => p.Id == permission.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.Name, permission.Name));

            if (result == 0)
                throw new AppException(ExceptionType.NotFound, "PermissionNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Permissions
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new AppException(ExceptionType.NotFound, "PermissionNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<PermissionEntity, bool>> predicate)
        => await _context.Permissions.AnyAsync(predicate);

    public async Task<ICollection<PermissionEntity>> FindAsync(Expression<Func<PermissionEntity, bool>> predicate)
        => await _context.Permissions
            .Include(p => p.Roles)
            .Where(predicate)
            .ToListAsync();
}
