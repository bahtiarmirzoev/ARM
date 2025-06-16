using System.Security.Claims;
using ARM.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ARM.Common.Extensions;

public static class HttpContextExtensions
{
    private static string GetClaimValue(this HttpContext httpContext, string claimType)
    {
        var value = httpContext.User.FindFirst(claimType)?.Value;
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");
        return value;
    }
    public static string GetUserId(this HttpContext httpContext)
        => httpContext.GetClaimValue("userId");
    public static string GetCustomerId(this HttpContext httpContext)
        => httpContext.GetClaimValue("customerId");

    public static string GetAutoServiceId(this HttpContext httpContext)
        => httpContext.GetClaimValue("autoServiceId");

    public static string GetPanel(this HttpContext httpContext)
        => httpContext.GetClaimValue("panel");

    public static string GetEmail(this HttpContext httpContext)
        => httpContext.GetClaimValue(ClaimTypes.Email);
}