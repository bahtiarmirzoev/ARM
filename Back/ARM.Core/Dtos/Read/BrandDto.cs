namespace ARM.Core.Dtos.Read;

public class BrandDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public bool IsOpen { get; set; }
    public int MaxCarsPerDay { get; set; }
    public bool HasParking { get; set; }
    public bool HasWaitingRoom { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<VenueDto> Venues { get; set; } = [];
    public ICollection<WorkingHourDto> WorkingHours { get; set; } = [];
}
