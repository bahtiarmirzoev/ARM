using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);
        
        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(v => v.VenueId).HasMaxLength(24);
        builder.Property(a => a.AutoServiceId).HasMaxLength(24);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.Price).HasPrecision(10, 2);
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.Rating).HasPrecision(3, 2);
        
        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.AutoServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.AutoRepairs)
            .WithMany(x => x.Services);
            
        builder.HasMany(x => x.ServiceRequests)
            .WithOne()
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(x => new { x.AutoServiceId, x.Name }).IsUnique();
        builder.HasIndex(x => x.Price);
        builder.HasIndex(x => x.Rating);
    }
}
