namespace ARM.Core.Dtos.Read;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime RegisteredAt { get; set; }
    public bool IsActive { get; set; }
    public bool EmailVerified { get; set; }
    public string? ProfilePicture { get; set; }
    public RoleDto Role { get; set; } = null!;
    public string BrandId { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
}