namespace ARM.Core.Dtos.Auth;

public class AutoServiceLoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Tp { get; set; } = string.Empty; // "ap" for admin, "mp" for manager
} 