using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Auth;

public class UserActiveSessionsEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string UserId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public string DeviceInfo { get; set; } = string.Empty;
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}