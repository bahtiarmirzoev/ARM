using ARM.Core.Enums;

namespace ARM.Core.Dtos.Read;

public class ServiceRequestDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string TechnicalPassport { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string ProblemDescription { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    public string CarPlate { get; set; } = string.Empty;
    public int? Year { get; set; }
    public DateTime PreferredDate { get; set; }
    public DateTime RequestDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public BrandDto Brand { get; set; } = null!;
    public bool IsProcessed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}