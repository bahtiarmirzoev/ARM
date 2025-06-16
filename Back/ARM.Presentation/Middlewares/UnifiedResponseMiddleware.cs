using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Services.Main;

namespace ARM.Presentation.Middlewares;

public class UnifiedResponseMiddleware
{
    private readonly RequestDelegate _next;

    public UnifiedResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }
        
        if (context.Request.Method == HttpMethods.Get)
        {
            await _next(context);
            return;
        }

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);

            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();
            context.Response.Body = originalBodyStream;

            var wrapped = WrapSuccess(context, responseText);
            await context.Response.WriteAsync(wrapped);
        }
        catch (AppException ex)
        {
            context.Response.Body = originalBodyStream;
            await HandleAppException(context, ex);
        }
        catch (Exception ex)
        {
            context.Response.Body = originalBodyStream;
            await HandleUnhandledException(context, ex);
        }
    }

    private string WrapSuccess(HttpContext context, string original)
    {
        var data = DeserializeAndConvertToArray(original ?? "{}");
        var meta = BuildMeta(context, context.Response.StatusCode);
        var metaArray = ConvertObjectToArray(meta);

        var responseArray = new[]
        {
            
            data,
            "ok",
            metaArray,
        };

        return JsonSerializer.Serialize(responseArray, JsonOpts());
    }

    private async Task HandleAppException(HttpContext context, AppException ex)
    {
        var code = GetStatusCodeForExceptionType(ex.ExceptionType);
        var meta = BuildMeta(context, code);
        var metaArray = ConvertObjectToArray(meta);

        var errorArray = new[]
        {
            ex.Message,
            ex.ExceptionType.ToString(),
            metaArray,
            "fail"
        };

        context.Response.StatusCode = code;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorArray, JsonOpts()));
    }

    private async Task HandleUnhandledException(HttpContext context, Exception ex)
    {
        var meta = BuildMeta(context, 500);
        var metaArray = ConvertObjectToArray(meta);

        var errorArray = new[]
        {
            500,
            ex.Message,
            "InternalServerError",
            metaArray,
            "fail"
        };

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorArray, JsonOpts()));
    }

    private object BuildMeta(HttpContext context, int statusCode)
    {
        return new
        {
            status = statusCode,
            timestamp = DateTime.UtcNow.ToString("o"),
            unix = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            execMs = DateTime.UtcNow.Millisecond,
            req = Guid.NewGuid(),
        };
    }

    private JsonSerializerOptions JsonOpts() => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private static int GetStatusCodeForExceptionType(ExceptionType exceptionType)
    {
        return exceptionType switch
        {
            ExceptionType.InvalidToken => (int)HttpStatusCode.BadRequest,
            ExceptionType.InvalidRefreshToken => (int)HttpStatusCode.BadRequest,
            ExceptionType.InvalidCredentials => (int)HttpStatusCode.BadRequest,
            ExceptionType.UserNotFound => (int)HttpStatusCode.NotFound,
            ExceptionType.NullCredentials => (int)HttpStatusCode.BadRequest,
            ExceptionType.InvalidRequest => (int)HttpStatusCode.BadRequest,
            ExceptionType.PasswordMismatch => (int)HttpStatusCode.BadRequest,
            ExceptionType.EmailAlreadyConfirmed => (int)HttpStatusCode.BadRequest,
            ExceptionType.EmailNotConfirmed => (int)HttpStatusCode.BadRequest,
            ExceptionType.EmailAlreadyExists => (int)HttpStatusCode.BadRequest,
            ExceptionType.CredentialsAlreadyExists => (int)HttpStatusCode.BadRequest,
            ExceptionType.NotFound => (int)HttpStatusCode.NotFound,
            ExceptionType.UnauthorizedAccess => (int)HttpStatusCode.Unauthorized,
            ExceptionType.Forbidden => (int)HttpStatusCode.Forbidden,
            ExceptionType.BadRequest => (int)HttpStatusCode.BadRequest,
            ExceptionType.Conflict => (int)HttpStatusCode.Conflict,
            ExceptionType.InternalServerError => (int)HttpStatusCode.InternalServerError,
            ExceptionType.ServiceUnavailable => (int)HttpStatusCode.ServiceUnavailable,
            ExceptionType.OperationFailed => (int)HttpStatusCode.BadRequest,
            ExceptionType.Validation => (int)HttpStatusCode.UnprocessableContent,
            ExceptionType.DatabaseError => (int)HttpStatusCode.InternalServerError,
            ExceptionType.Critical => (int)HttpStatusCode.InternalServerError,
            _ => (int)HttpStatusCode.InternalServerError,
        };
    }

    private object DeserializeAndConvertToArray(string json)
    {
        var doc = JsonDocument.Parse(json);
        return ConvertJsonElementToArray(doc.RootElement);
    }

    private object ConvertObjectToArray(object obj)
    {
        // Для анонимных объектов (типа мета) — сериализуем в JsonDocument и конвертируем
        var json = JsonSerializer.Serialize(obj, JsonOpts());
        using var doc = JsonDocument.Parse(json);
        return ConvertJsonElementToArray(doc.RootElement);
    }

    private object ConvertJsonElementToArray(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                // Обход всех свойств, собираем их значения в массив
                var list = new List<object>();
                foreach (var prop in element.EnumerateObject())
                {
                    list.Add(ConvertJsonElementToArray(prop.Value));
                }
                return list.ToArray();

            case JsonValueKind.Array:
                var arr = new List<object>();
                foreach (var item in element.EnumerateArray())
                {
                    arr.Add(ConvertJsonElementToArray(item));
                }
                return arr.ToArray();

            case JsonValueKind.String:
                return element.GetString();

            case JsonValueKind.Number:
                if (element.TryGetInt64(out long l)) return l;
                if (element.TryGetDouble(out double d)) return d;
                return null;

            case JsonValueKind.True:
                return true;

            case JsonValueKind.False:
                return false;

            case JsonValueKind.Null:
                return null;

            default:
                return null;
        }
    }
}