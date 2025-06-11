using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class VenueEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOpen { get; set; } = true;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<ServiceEntity> Services { get; set; } = [];
    public string BrandId { get; set; } = string.Empty;
    public BrandEntity Brand { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

}