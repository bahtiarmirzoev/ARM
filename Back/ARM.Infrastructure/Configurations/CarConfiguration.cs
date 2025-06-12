using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(a => a.OwnerId).HasMaxLength(24);
        builder.Property(x => x.Make).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Model).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CarPlate).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Year).IsRequired();
        builder.Property(x => x.Color).HasMaxLength(30);
        builder.Property(x => x.VIN).HasMaxLength(17);
        builder.Property(x => x.EngineType).HasMaxLength(30);
        builder.Property(x => x.EngineVolume).HasPrecision(3, 1);
        builder.Property(x => x.Transmission).HasMaxLength(30);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.RepairHistory)
            .WithOne(x => x.Car)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CarPlate).IsUnique();
        builder.HasIndex(x => x.VIN).IsUnique();
    }
}