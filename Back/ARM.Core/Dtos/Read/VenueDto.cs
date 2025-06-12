namespace ARM.Core.Dtos.Read;

public class VenueDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOpen { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<ServiceDto> Services { get; set; } = [];
}