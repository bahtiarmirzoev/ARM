using System.Security.Claims;

namespace ARM.Core.Constants;

public static class ClaimKeys
{
    public const string UserId = "user";
    public const string CustomerId = "customer";
    public const string BrandId = "brand";
    public const string Panel = "panel";
    public const string Role = ClaimTypes.Role;
    public const string RoleId = "roleId";
    public const string Email = ClaimTypes.Email;
}