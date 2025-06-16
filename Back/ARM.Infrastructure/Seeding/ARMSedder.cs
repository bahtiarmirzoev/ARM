using ARM.Core.Entities.Main;
using ARM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static NanoidDotNet.Nanoid;
using static BCrypt.Net.BCrypt;

namespace ARM.Infrastructure.Seeding;
public class ARMSedder
{
    private readonly ARMContext _context;

    public ARMSedder(ARMContext context) => _context = context;

    public async Task SeedAsync()
    {
        await _context.Database.MigrateAsync();
        
        if (!await _context.Roles.AnyAsync())
        {
            var roles = new[]
            {
                new RoleEntity { Id = Generate(size: 24), Name = "Admin", Description = "AR" },
                new RoleEntity { Id = Generate(size: 24), Name = "Manager", Description = "MR" }
            };
            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }

        var superAdminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "SuperAdmin");

        if (superAdminRole is null)
        {
            superAdminRole = new RoleEntity
            {
                Id = Generate(size: 24),
                Name = "SuperAdmin",
                Description = "SAR"
            };
            await _context.Roles.AddAsync(superAdminRole);
            await _context.SaveChangesAsync();
        }
        bool superAdminExists = await _context.Users.AnyAsync(u => u.RoleId == superAdminRole.Id);
        if (!superAdminExists)
        {
            var superAdmin = new UserEntity
            {
                Id = Generate(size: 24),
                Name = "Super",
                Surname = "Admin",
                Email = "magsudluefgan@example.com",
                Password = HashPassword("ChangeMe123!"), 
                RoleId = superAdminRole.Id,
                IsActive = true,
                EmailVerified = true,
                BrandId = null
            };
            await _context.Users.AddAsync(superAdmin);
            await _context.SaveChangesAsync();
        }
    }
}