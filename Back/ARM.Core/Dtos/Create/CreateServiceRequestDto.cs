namespace ARM.Core.Dtos.Create;

public class CreateServiceRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string TechnicalPassport { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string ProblemDescription { get; set; } = string.Empty;
    public string CarPlate { get; set; } = string.Empty;
    public int? Year { get; set; }
    public DateTime PreferredDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string AutoRepairId { get; set; } = string.Empty;
}