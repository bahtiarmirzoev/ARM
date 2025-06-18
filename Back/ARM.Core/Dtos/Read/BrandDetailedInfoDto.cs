namespace ARM.Core.Dtos.Read;

public class BrandDetailedInfoDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public int TotalReviews { get; set; }
    public bool IsOpen { get; set; }
    public int MaxCarsPerDay { get; set; }
    public bool HasParking { get; set; }
    public bool HasWaitingRoom { get; set; }
    public decimal BasePrice { get; set; }
    public decimal PremiumPrice { get; set; }
    public ICollection<ServiceDto> Services { get; set; } = [];
    public ICollection<WorkingHourDto> WorkingHours { get; set; } = [];
    public ICollection<VenueDto> Venues { get; set; } = [];
    public ICollection<ReviewDto> Reviews { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}