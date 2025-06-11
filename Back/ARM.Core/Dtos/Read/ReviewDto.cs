namespace ARM.Core.Dtos.Read;

public class ReviewDto
{
    public string Id { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string AutoServiceId { get; set; } = string.Empty;
    public string AutoServiceName { get; set; } = string.Empty;
    public PublicUserDto User { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}