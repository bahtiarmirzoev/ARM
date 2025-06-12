using ARM.Core.Dtos.Update;
using FluentValidation;

namespace ARM.Application.Validators.Update;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerValidator()
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