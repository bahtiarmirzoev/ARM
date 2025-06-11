using ARM.Core.Dtos.Create;
using FluentValidation;

namespace ARM.Application.Validators.Create;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("NameRequired");

        RuleFor(user => user.Surname)
            .NotEmpty()
            .WithMessage("SurnameRequired");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("EmailRequired")
            .EmailAddress()
            .WithMessage("InvalidEmail");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("PasswordRequired")
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
            .WithMessage("InvalidPassword");
    }
}