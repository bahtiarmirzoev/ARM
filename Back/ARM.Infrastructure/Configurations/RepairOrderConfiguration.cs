using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class RepairOrderConfiguration : IEntityTypeConfiguration<RepairOrderEntity>
{
    public void Configure(EntityTypeBuilder<RepairOrderEntity> builder)
    {
        builder.HasKey(ro => ro.Id);

        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(a => a.AutoServiceId).HasMaxLength(24);
        builder.Property(a => a.CarId).HasMaxLength(24);
        builder.Property(a => a.ServiceTypeId).HasMaxLength(24);
        builder.Property(a => a.CustomerId).HasMaxLength(24);
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.ScheduledDate).IsRequired();
        builder.Property(x => x.EstimatedDuration).IsRequired();
        builder.Property(x => x.DiagnosisResults).HasMaxLength(2000);
        builder.Property(x => x.EstimatedCost).HasPrecision(10, 2);
        builder.Property(x => x.ActualCost).HasPrecision(10, 2);
        builder.Property(x => x.CustomerComments).HasMaxLength(1000);
        builder.Property(x => x.CancellationReason).HasMaxLength(500);
        builder.Property(x => x.ServiceStatus).IsRequired().HasConversion<string>();

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.RepairOrders)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Car)
            .WithMany()
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.AutoServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.OrderDate);
        builder.HasIndex(x => x.ScheduledDate);
        builder.HasIndex(x => x.ServiceStatus);
        builder.HasIndex(x => new { x.CustomerId, x.ServiceStatus });
    }
}
