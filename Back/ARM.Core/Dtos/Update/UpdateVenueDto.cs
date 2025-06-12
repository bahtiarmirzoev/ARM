namespace ARM.Core.Dtos.Update;

public class UpdateVenueDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOpen { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string BrandId { get; set; } = string.Empty;
}