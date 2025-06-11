using ARM.Core.Enums;

namespace ARM.Core.Dtos.Update;

public class UpdateServiceRequestDto
{
    public RequestStatus Status { get; set; }
    public bool IsProcessed { get; set; }
    public string? ProblemDescription { get; set; }
}