using System;

namespace ARM.Core.Dtos.Read;

public class ReviewDto
{
    public string Id { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public CustomerDto Customer { get; set; } = null!;
    public string AutoServiceId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}