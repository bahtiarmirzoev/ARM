using ARM.Core.Dtos.Create;
using FluentValidation;

namespace ARM.Application.Validators.Create;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("NameValidationError");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("SurnameValidationError");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100)
            .WithMessage("EmailValidationError");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100)
            .Matches("[A-Z]").WithMessage("PasswordUppercaseValidationError")
            .Matches("[a-z]").WithMessage("PasswordLowercaseValidationError")
            .Matches("[0-9]").WithMessage("PasswordNumberValidationError")
            .Matches("[^a-zA-Z0-9]").WithMessage("PasswordSpecialCharValidationError")
            .WithMessage("PasswordValidationError");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .MaximumLength(20)
            .WithMessage("PhoneNumberValidationError");

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("AddressValidationError");

        RuleFor(x => x.ProfilePicture)
            .MaximumLength(500)
            .When(x => x.ProfilePicture != null)
            .WithMessage("ProfilePictureValidationError");
    }
}