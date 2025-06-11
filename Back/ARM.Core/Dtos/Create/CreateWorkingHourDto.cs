namespace ARM.Core.Dtos.Create;

public class CreateWorkingHourDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public bool IsDayOff { get; set; }
} 