using ARM.Common.Exceptions;
using FluentValidation;

namespace ARM.Common.Extensions;

public static class ValidationExtension
{
    public static async Task ValidateAndThrowAsync<T>(this IValidator<T> validator, T instance)
    {
        var validationResult = await validator.ValidateAsync(instance);
    
        if (!validationResult.IsValid)
            throw new AppException(ExceptionType.Validation, validationResult.Errors.Select(e => e.ErrorMessage)
                .FirstOrDefault());
    }
}