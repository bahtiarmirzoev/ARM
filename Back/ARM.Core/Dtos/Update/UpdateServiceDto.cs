﻿namespace ARM.Core.Dtos.Update;

public class UpdateServiceDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsActive { get; set; }
}