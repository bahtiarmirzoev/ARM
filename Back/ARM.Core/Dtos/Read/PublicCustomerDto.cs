namespace ARM.Core.Dtos.Read;

public class PublicCustomerDto 
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool EmailVerified { get; set; }
    public string ProfilePicture { get; set; } = string.Empty;
}