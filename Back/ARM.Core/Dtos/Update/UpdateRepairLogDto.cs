namespace ARM.Core.Dtos.Update;

public class UpdateRepairLogDto
{
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
    public ICollection<string> Services { get; set; } = [];
}
