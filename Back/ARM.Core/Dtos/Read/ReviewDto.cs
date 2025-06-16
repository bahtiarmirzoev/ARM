using System;

namespace ARM.Core.Dtos.Read;

public class ReviewDto
{
    public string Id { get; set; } = string.Empty;
    public PublicCustomerDto Customer { get; set; } = null!;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}