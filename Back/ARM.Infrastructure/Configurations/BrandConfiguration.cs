using System.Text.Json;
using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Latitude).HasPrecision(10, 6);
        builder.Property(x => x.Longitude).HasPrecision(10, 6);
        builder.Property(x => x.IsOpen).IsRequired().HasDefaultValue(true);
        
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
        builder.HasIndex(x => new { x.Latitude, x.Longitude });

        builder.Property(ar => ar.MaxCarsPerDay).IsRequired().HasDefaultValue(0);
        builder.Property(ar => ar.HasParking).IsRequired().HasDefaultValue(false);
        builder.Property(ar => ar.HasWaitingRoom).IsRequired().HasDefaultValue(false);

        builder.HasMany(ar => ar.Services)
            .WithOne(s => s.Brand)
            .HasForeignKey(s => s.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ar => ar.Reviews)
            .WithOne()
            .HasForeignKey(r => r.AutoServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ar => ar.WorkingHours)
            .WithOne(wh => wh.Brand)
            .HasForeignKey(wh => wh.AutoServiceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(b => b.Venues)
            .WithOne(v => v.Brand)
            .HasForeignKey(v => v.BrandId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}