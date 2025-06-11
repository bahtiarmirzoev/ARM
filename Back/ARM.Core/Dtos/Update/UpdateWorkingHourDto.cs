namespace ARM.Core.Dtos.Update;

public class UpdateWorkingHourDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public bool IsDayOff { get; set; }
} 