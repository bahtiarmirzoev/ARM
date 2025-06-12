using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class CarEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string CarPlate { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public string VIN { get; set; } = string.Empty;
    public string EngineType { get; set; } = string.Empty;
    public double EngineVolume { get; set; }
    public string Transmission { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
    public CustomerEntity Customer { get; set; } = null!;
    public ICollection<RepairLogEntity> RepairHistory { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}