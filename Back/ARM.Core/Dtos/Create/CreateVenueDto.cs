namespace ARM.Core.Dtos.Create;

public class CreateVenueDto
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsOpen { get; set; } = true;
    public string BrandId { get; set; } = null!;
}