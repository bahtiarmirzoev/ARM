using System;
using System.Collections.Generic;
using ARM.Core.Enums;

namespace ARM.Core.Dtos.Read;

public class RepairOrderDto
{
    public string Id { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public CustomerDto Customer { get; set; } = null!;
    public string CarId { get; set; } = string.Empty;
    public CarDto Car { get; set; } = null!;
    public string AutoServiceId { get; set; } = string.Empty;
    public string ServiceTypeId { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public string? DiagnosisResults { get; set; }
    public decimal ActualCost { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime OrderDate { get; set; }
    public string ServiceStatus { get; set; } = string.Empty;
    public decimal EstimatedCost { get; set; }
    public string? CustomerComments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
