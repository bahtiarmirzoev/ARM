using ARM.Core.Dtos.Update;
using FluentValidation;

namespace ARM.Application.Validators.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("NameRequired");

        RuleFor(user => user.Surname)
            .NotEmpty()
            .WithMessage("SurnameRequired");
    }
}