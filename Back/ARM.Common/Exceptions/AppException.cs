namespace ARM.Common.Exceptions;

public class AppException : Exception
{
    public ExceptionType ExceptionType { get; set; }
    public AppException(ExceptionType exceptionType, string message) : base(message)
        => ExceptionType = exceptionType;
}