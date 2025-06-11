using ARM.Core.Entities;
using ARM.Core.Entities.Auth;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace ARM.Infrastructure.Context;

public class ARMContext : DbContext
{
    public ARMContext(DbContextOptions<ARMContext> options) : base(options){}
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<WorkingHourEntity> WorkingHours { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<ServiceRequestEntity> MakeOrderRequests { get; set; }
    public DbSet<RepairLogEntity> RepairLogs { get; set; }
    public DbSet<RepairOrderEntity> RepairOrders { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<BlackListedEntity> BlackListeds { get; set; }
    public DbSet<UserActiveSessionsEntity> UserActiveSessions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ARMContext).Assembly);
}