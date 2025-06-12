namespace ARM.Common.Exceptions;

public enum ExceptionType
{
    InvalidToken = 1, 
    InvalidRefreshToken, 
    InvalidCredentials,
    UserNotFound, 
    NullCredentials,
    InvalidRequest,
    PasswordMismatch,
    EmailAlreadyConfirmed,
    EmailNotConfirmed, 
    EmailAlreadyExists,
    CredentialsAlreadyExists,
    NotFound,
    UnauthorizedAccess,
    Forbidden,
    BadRequest,
    Conflict,
    InternalServerError, 
    ServiceUnavailable,
    OperationFailed,
    DatabaseError,
    Critical,
    Validation
}