using ARM.Common.Exceptions;

namespace ARM.Common.Extensions;

public static class GuardExtension
{
    public static T EnsureFound<T>(this T? entity, string errorMessage)
        where T : class
    => entity ?? throw new AppException(ExceptionType.NotFound, errorMessage);
}