using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class PermissionEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public virtual ICollection<RoleEntity> Roles { get; set; } = [];
    public virtual ICollection<UserEntity> Users { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}