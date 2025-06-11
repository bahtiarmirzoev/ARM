using ARM.Core.Enums;

namespace ARM.Core.Dtos.Update;

public class UpdateRepairOrderDto
{
    public DateTime ScheduledDate { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public string DiagnosisResults { get; set; } = string.Empty;
    public decimal ActualCost { get; set; }
    public decimal EstimatedCost { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
    public ServiceStatus ServiceStatus { get; set; }
    public string CustomerComments { get; set; } = string.Empty;
}
