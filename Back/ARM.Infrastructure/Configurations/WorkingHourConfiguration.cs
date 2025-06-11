using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHourEntity>
{
    public void Configure(EntityTypeBuilder<WorkingHourEntity> builder)
    {
        builder.ToTable("WorkingHours");
        builder.HasKey(wh => wh.Id);

        builder.Property(wh => wh.Id).HasMaxLength(24);
        builder.Property(wh => wh.Day).IsRequired();
        builder.Property(wh => wh.OpenTime).IsRequired();
        builder.Property(wh => wh.CloseTime).IsRequired();

        builder.Property(wh => wh.AutoServiceId)
            .IsRequired()
            .HasMaxLength(24);

        builder.HasIndex(wh => new { wh.AutoServiceId, wh.Day }).IsUnique();
    }
}