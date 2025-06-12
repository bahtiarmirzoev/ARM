using ARM.Core.Dtos.Create;
using FluentValidation;

namespace ARM.Application.Validators.Create;

public class CreateVenueValidator : AbstractValidator<CreateVenueDto>
{
    public CreateVenueValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("VenueNameRequired")
            .MaximumLength(100).WithMessage("VenueNameTooLong");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("VenueAddressRequired")
            .MaximumLength(200).WithMessage("VenueAddressTooLong");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("VenuePhoneRequired")
            .MaximumLength(20).WithMessage("VenuePhoneTooLong")
            .Matches(@"^\+?[0-9\s\-\(\)]+$").WithMessage("VenuePhoneInvalidFormat");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("VenueEmailRequired")
            .MaximumLength(100).WithMessage("VenueEmailTooLong")
            .EmailAddress().WithMessage("VenueEmailInvalidFormat");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90.0, 90.0).WithMessage("VenueLatitudeInvalid");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180.0, 180.0).WithMessage("VenueLongitudeInvalid");
        
    }
}