namespace ARM.Core.Dtos.Read;

public class WorkingHourDto
{
    public string Day { get; set; } = string.Empty;
    public bool IsDayOff { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
}