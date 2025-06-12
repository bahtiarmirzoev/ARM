using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequestEntity>
{
    public void Configure(EntityTypeBuilder<ServiceRequestEntity> builder)
    {
        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(a => a.AutoRepairId).HasMaxLength(24);
        builder.Property(a => a.ServiceId).HasMaxLength(24);
        builder.Property(a => a.UserId).HasMaxLength(24);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
        builder.Property(x => x.TechnicalPassport).HasMaxLength(50);
        builder.Property(x => x.Make).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Model).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ProblemDescription).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.CarPlate).HasMaxLength(20);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.Status).IsRequired().HasConversion<string>();
        
        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.AutoRepairId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(x => x.Service)
            .WithMany(x => x.ServiceRequests)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(x => x.RequestDate);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.ServiceId);
        builder.HasIndex(x => x.UserId);
    }
}