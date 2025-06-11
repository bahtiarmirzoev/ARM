using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class RepairLogConfiguration : IEntityTypeConfiguration<RepairLogEntity>
{
    public void Configure(EntityTypeBuilder<RepairLogEntity> builder)
    {
        builder.ToTable("RepairLogs");
        builder.HasKey(r => r.Id); 

        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(a => a.AutoServiceId).HasMaxLength(24);
        builder.Property(a => a.CarId).HasMaxLength(24);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.Cost).HasPrecision(10, 2);
        builder.Property(x => x.Diagnosis).HasMaxLength(1000);
        builder.Property(x => x.WorkPerformed).HasMaxLength(2000);
        builder.Property(x => x.PartsReplaced).HasMaxLength(1000);
        builder.Property(x => x.Recommendations).HasMaxLength(1000);
        builder.Property(x => x.PartsCost).HasPrecision(10, 2);
        builder.Property(x => x.LaborCost).HasPrecision(10, 2);
        builder.Property(x => x.Mileage).IsRequired();
        
        builder.HasOne(x => x.Car)
            .WithMany(x => x.RepairHistory)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.AutoServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Services)
            .WithMany();
        
        builder.HasIndex(x => x.RepairDate);
        builder.HasIndex(x => new { x.CarId, x.RepairDate });
    }
}
