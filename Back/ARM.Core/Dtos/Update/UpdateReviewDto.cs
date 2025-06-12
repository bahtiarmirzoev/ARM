namespace ARM.Core.Dtos.Update;

public class UpdateReviewDto
{
    public string CustomerId { get; set; } = string.Empty;
    public string AutoServiceId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
