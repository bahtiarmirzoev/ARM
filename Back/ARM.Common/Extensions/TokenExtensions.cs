using Microsoft.AspNetCore.Http;

namespace ARM.Common.Extensions;

public static class TokenExtensions
{
    public static bool HasValidTokens(this HttpContext context)
    {
        var atk = context.GetAccessToken();
        var rtk = context.GetRefreshToken();
        return !string.IsNullOrEmpty(atk) && !string.IsNullOrEmpty(rtk);
    }
}