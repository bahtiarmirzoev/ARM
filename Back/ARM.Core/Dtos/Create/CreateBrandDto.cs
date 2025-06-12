using ARM.Core.Dtos.Read;

namespace ARM.Core.Dtos.Create;

public class CreateBrandDto
{
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<CreateWorkingHourDto> WorkingHours { get; set; } = [];
    public ICollection<CreateServiceDto> Services { get; set; } = [];
    public ICollection<CreateVenueDto> Venues { get; set; } = [];
    public int MaxCarsPerDay { get; set; }
    public bool HasParking { get; set; }
    public bool HasWaitingRoom { get; set; }
    public string TaxNumber { get; set; } = string.Empty;
    public string BankAccount { get; set; } = string.Empty;
}
