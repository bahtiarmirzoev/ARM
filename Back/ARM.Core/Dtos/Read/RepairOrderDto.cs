using ARM.Core.Enums;

namespace ARM.Core.Dtos.Read;

public class RepairOrderDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public DateTime ScheduledDate { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public string DiagnosisResults { get; set; } = string.Empty;
    public decimal EstimatedCost { get; set; }
    public decimal ActualCost { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
    public ServiceStatus ServiceStatus { get; set; }
    public string CustomerComments { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
    public CarDto Car { get; set; } = null!;
    public BrandDto Brand { get; set; } = null!;
    public ServiceTypeDto ServiceType { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
