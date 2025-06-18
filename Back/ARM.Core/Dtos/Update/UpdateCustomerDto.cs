namespace ARM.Core.Dtos.Update;

public class UpdateCustomerDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool TwoFaEnabled { get; set; }
    public string? ProfilePicture { get; set; }
}