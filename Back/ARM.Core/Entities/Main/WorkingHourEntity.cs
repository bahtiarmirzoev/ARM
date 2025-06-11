using static NanoidDotNet.Nanoid;

namespace ARM.Core.Entities.Main;

public class WorkingHourEntity
{
    public string Id { get; set; } = Generate(size: 24);
    public DayOfWeek Day { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public bool IsDayOff { get; set; }
    public string AutoServiceId { get; set; } = string.Empty;
    public BrandEntity Brand { get; set; } = null!;
}