using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(a => a.AutoServiceId).HasMaxLength(24);
        builder.Property(a => a.CustomerId).HasMaxLength(24);
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.Comment).HasMaxLength(1000);

        builder.HasOne(r => r.Customer)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Brand)
            .WithMany(a => a.Reviews)
            .HasForeignKey(r => r.AutoServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}