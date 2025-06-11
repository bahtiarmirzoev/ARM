using ARM.Core.Enums;
using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class ServiceRequestEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string TechnicalPassport { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string ProblemDescription { get; set; } = string.Empty;
    public RequestStatus Status { get; set; } = RequestStatus.New;
    public string CarPlate { get; set; } = string.Empty;
    public int? Year { get; set; }
    public DateTime PreferredDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
    public string AutoRepairId { get; set; } = string.Empty;
    public BrandEntity Brand { get; set; } = null!;
    public string ServiceId { get; set; } = string.Empty;
    public ServiceEntity Service { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public UserEntity User { get; set; } = null!;
    public bool IsProcessed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
