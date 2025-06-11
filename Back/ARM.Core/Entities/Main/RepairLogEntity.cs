using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;
public class RepairLogEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string CarId { get; set; } = string.Empty;
    public string AutoServiceId { get; set; } = string.Empty;
    public DateTime RepairDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string WorkPerformed { get; set; } = string.Empty;
    public string PartsReplaced { get; set; } = string.Empty;
    public string Recommendations { get; set; } = string.Empty;
    public decimal PartsCost { get; set; }
    public decimal LaborCost { get; set; }
    public CarEntity Car { get; set; } = null!;
    public BrandEntity Brand { get; set; } = null!;
    public ICollection<ServiceEntity> Services { get; set; } = [];
    public int Mileage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
