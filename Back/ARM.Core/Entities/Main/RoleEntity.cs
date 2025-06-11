using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class RoleEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<PermissionEntity> Permissions { get; set; } = [];
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}