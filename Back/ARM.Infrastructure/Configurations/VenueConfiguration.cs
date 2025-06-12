using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<VenueEntity>
{
    public void Configure(EntityTypeBuilder<VenueEntity> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id).HasMaxLength(24);
        builder.Property(v => v.BrandId).HasMaxLength(24);
        builder.Property(v => v.Name).IsRequired().HasMaxLength(100);
        builder.Property(v => v.Address).IsRequired().HasMaxLength(200);
        builder.Property(v => v.PhoneNumber).HasMaxLength(20);
        builder.Property(v => v.Email).HasMaxLength(100);
        builder.Property(v => v.Latitude).HasPrecision(10, 6);
        builder.Property(v => v.Longitude).HasPrecision(10, 6);
        builder.Property(v => v.IsOpen).IsRequired().HasDefaultValue(true);

        builder.HasMany(v => v.Services)
            .WithMany(s => s.Venues)
            .UsingEntity(j => j.ToTable("VenueServices"));

        builder.HasOne(v => v.Brand)
            .WithMany(b => b.Venues)
            .HasForeignKey(v => v.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(v => v.BrandId);
        builder.HasIndex(v => new { v.Latitude, v.Longitude });
    }
}