using ARM.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using static ARM.Core.Constants.ClaimKeys;

namespace ARM.Common.Extensions;

public static class HttpContextExtensions
{
    private static string GetClaimValue(this HttpContext httpContext, string claimType)
        =>  string.IsNullOrWhiteSpace(httpContext.User.FindFirst(claimType)?.Value)
            ? throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized")
            : httpContext.User.FindFirst(claimType)!.Value;
    public static string GetUserId(this HttpContext httpContext)
        => httpContext.GetClaimValue(UserId);
    public static string GetCustomerId(this HttpContext httpContext)
        => httpContext.GetClaimValue(CustomerId);
    public static string GetBrandId(this HttpContext httpContext)
        => httpContext.GetClaimValue(BrandId);
    public static string GetPanel(this HttpContext httpContext)
        => httpContext.GetClaimValue(Panel);
    public static string GetUserRole(this HttpContext httpContext)
        => httpContext.GetClaimValue(Role);
    public static string GetRoleId(this HttpContext httpContext)
        => httpContext.GetClaimValue(RoleId);
    public static string GetEmail(this HttpContext httpContext)
        => httpContext.GetClaimValue(Email);
}