using ARM.Core.Dtos.Read;

namespace ARM.Core.Dtos.Update;

public class UpdateBrandDto
{
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TaxNumber { get; set; } = string.Empty;
    public string BankAccount { get; set; } = string.Empty;
    public ICollection<WorkingHourDto> WorkingHours { get; set; } = [];
    public ICollection<ServiceDto> Services { get; set; } = [];
    public int MaxCarsPerDay { get; set; }
    public bool HasParking { get; set; }
    public bool HasWaitingRoom { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
