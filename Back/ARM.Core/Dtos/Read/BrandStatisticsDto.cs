namespace ARM.Core.Dtos.Read;

public class BrandStatisticsDto
{
    public int TotalServices { get; set; }
    public int ActiveServices { get; set; }
    public int TotalRequests { get; set; }
    public int CompletedRequests { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageRevenue { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public bool IsOpen { get; set; }
    public int MaxCarsPerDay { get; set; }
    public ICollection<ServiceDto> Services { get; set; } = [];
    public ICollection<WorkingHourDto> WorkingHours { get; set; } = [];
    public ICollection<VenueDto> Venues { get; set; } = [];
}