using Microsoft.AspNetCore.Http;

namespace ARM.Common.Extensions;

public static class HttpRequestExtensions
{
    public static string GetDeviceInfo(this HttpRequest request)
        => request.Headers["User-Agent"].ToString();
    public static string GetIpAddress(this HttpContext context)
        => context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "Unknown";
}