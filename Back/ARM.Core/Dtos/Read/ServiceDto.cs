namespace ARM.Core.Dtos.Read;

public class ServiceDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsActive { get; set; }
    public decimal Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
