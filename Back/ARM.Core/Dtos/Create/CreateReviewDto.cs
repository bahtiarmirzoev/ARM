namespace ARM.Core.Dtos.Create;

public class CreateReviewDto
{
    public string AutoServiceId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
