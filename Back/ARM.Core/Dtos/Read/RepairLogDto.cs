namespace ARM.Core.Dtos.Read;

public class RepairLogDto
{
    public string Id { get; set; } = string.Empty;
    public string CarId { get; set; } = string.Empty;
    public string AutoRepairId { get; set; } = string.Empty;
    public DateTime RepairDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string WorkPerformed { get; set; } = string.Empty;
    public string PartsReplaced { get; set; } = string.Empty;
    public string Recommendations { get; set; } = string.Empty;
    public decimal PartsCost { get; set; }
    public decimal LaborCost { get; set; }
    public int Mileage { get; set; }
    public CarDto Car { get; set; } = null!;
    public BrandDto Brand { get; set; } = null!;
    public ICollection<ServiceDto> Services { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
