namespace ARM.Core.Dtos.Create;

public class CreateServiceDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsActive { get; set; } = true;
}
