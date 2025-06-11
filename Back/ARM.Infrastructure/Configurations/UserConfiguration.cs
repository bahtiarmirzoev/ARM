using ARM.Core.Entities;
using ARM.Core.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARM.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        
        builder.Property(a => a.Id).HasMaxLength(24);
        builder.Property(a => a.RoleId).HasMaxLength(24);
        builder.Property(a => a.BrandId).HasMaxLength(24);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Address).HasMaxLength(200);
        builder.Property(x => x.ProfilePicture).HasMaxLength(500);
        
        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Cars)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.RepairOrders)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
        builder.HasIndex(x => new { x.Name, x.Surname });
        builder.HasIndex(x => x.RoleId);
        builder.HasIndex(x => x.BrandId);
    }
}