﻿namespace ARM.Core.Dtos.Update;

public class UpdateCarDto
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string CarPlate { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public string VIN { get; set; } = string.Empty;
    public string EngineType { get; set; } = string.Empty;
    public double EngineVolume { get; set; }
    public string Transmission { get; set; } = string.Empty;
}
