namespace ARM.Core.Dtos.Create;

public class CreateReviewDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string AutoServiceId { get; set; } = string.Empty;
}
