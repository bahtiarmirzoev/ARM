using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class ReviewEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public CustomerEntity Customer { get; set; } = null!;
    public string AutoServiceId { get; set; } = string.Empty;
    public BrandEntity Brand { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}