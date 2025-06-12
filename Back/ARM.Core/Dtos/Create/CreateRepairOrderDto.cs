using System;

namespace ARM.Core.Dtos.Create;

public class CreateRepairOrderDto
{
    public string CustomerId { get; set; } = string.Empty;
    public string CarId { get; set; } = string.Empty;
    public string AutoServiceId { get; set; } = string.Empty;
    public string ServiceTypeId { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public string? DiagnosisResults { get; set; }
    public decimal EstimatedCost { get; set; }
    public string? CustomerComments { get; set; }
}
