using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(r => r.Id);

        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(a => a.AutoServiceId).HasMaxLength(24);
        builder.Property(a => a.UserId).HasMaxLength(24);
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.Comment).HasMaxLength(1000);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Brand)
            .WithMany(a => a.Reviews)
            .HasForeignKey(r => r.AutoServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}