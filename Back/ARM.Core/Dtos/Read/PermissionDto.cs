namespace ARM.Core.Dtos.Read;

public class PermissionDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<RoleDto> Roles { get; set; } = [];
    public IEnumerable<UserDto> Users { get; set; } = [];
}
