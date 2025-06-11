namespace ARM.Core.Dtos.Create;

public class CreateRepairOrderDto
{
    public string CarId { get; set; } = string.Empty;
    public string AutoServiceId { get; set; } = string.Empty;
    public string ServiceTypeName { get; set; } = string.Empty;
    public decimal ActualCost { get; set; } 
    public DateTime ScheduledDate { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public decimal EstimatedCost { get; set; }
    public string CustomerComments { get; set; } = string.Empty;
}
