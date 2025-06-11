using ARM.Core.Enums;
using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class UserEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? ProfilePicture { get; set; }
    public bool EmailVerified { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public ICollection<CarEntity> Cars { get; set; } = [];
    public ICollection<RepairOrderEntity> RepairOrders { get; set; } = [];
    public ICollection<ReviewEntity> Reviews { get; set; } = [];
    public string RoleId { get; set; } = string.Empty;
    public RoleEntity Role { get; set; } = null!;
    public string BrandId { get; set; } = string.Empty;
    public BrandEntity? Brand { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}