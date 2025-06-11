using System.Security.Claims;

namespace ARM.Common.Extensions;

public static class UserContextExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }
    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return user.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    }
}