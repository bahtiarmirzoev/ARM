using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class BrandEntity 
{
    public string Id { get; set; } = Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Rating { get; set; } = 5;
    public string Email { get; set; } = string.Empty;
    public int TotalReviews { get; set; }
    public ICollection<WorkingHourEntity> WorkingHours { get; set; } = [];
    public ICollection<VenueEntity> Venues { get; set; } = [];
    public int MaxCarsPerDay { get; set; }
    public bool HasParking { get; set; }
    public bool HasWaitingRoom { get; set; }
    public ICollection<ServiceEntity> Services { get; set; } = [];
    public ICollection<ReviewEntity> Reviews { get; set; } = [];
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsOpen { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}