using System;
using System.Collections.Generic;

namespace ARM.Core.Dtos.Read;

public class CustomerDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    public bool EmailVerified { get; set; }
    public bool TwoFaEnabled { get; set; }
    public ICollection<CarDto> Cars { get; set; } = [];
    public ICollection<RepairOrderDto> RepairOrders { get; set; } = [];
    public ICollection<ReviewDto> Reviews { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}