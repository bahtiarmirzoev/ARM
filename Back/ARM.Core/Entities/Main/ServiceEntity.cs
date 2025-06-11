using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class ServiceEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public string AutoServiceId { get; set; } = string.Empty;
    public BrandEntity Brand { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public decimal Rating { get; set; } = 5;
    public string VenueId { get; set; } = string.Empty; 
    public VenueEntity Venue { get; set; } = null!;
    public ICollection<BrandEntity> AutoRepairs { get; set; } = [];
    public ICollection<ServiceRequestEntity> ServiceRequests { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}