using ARM.Core.Enums;
using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class RepairOrderEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string CustomerId { get; set; } = string.Empty;
    public string CarId { get; set; } = string.Empty;
    public string AutoServiceId { get; set; } = string.Empty;
    public string ServiceTypeId { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public string DiagnosisResults { get; set; } = string.Empty;
    public decimal ActualCost { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public ServiceStatus ServiceStatus { get; set; } = ServiceStatus.NotDone;
    public decimal EstimatedCost { get; set; }
    public string CustomerComments { get; set; } = string.Empty;
    public CustomerEntity Customer { get; set; } = null!;
    public CarEntity Car { get; set; } = null!;
    public BrandEntity Brand { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
