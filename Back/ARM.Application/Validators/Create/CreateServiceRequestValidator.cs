using FluentValidation;
using ARM.Core.Dtos.Create;

namespace ARM.Application.Validators.Create;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequestDto>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(sr => sr.Name)
            .NotEmpty()
            .WithMessage("NameRequired");

        RuleFor(sr => sr.Phone)
            .NotEmpty()
            .WithMessage("PhoneRequired")
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("InvalidPhoneFormat");

        RuleFor(sr => sr.TechnicalPassport)
            .NotEmpty()
            .WithMessage("TechnicalPassportRequired");

        RuleFor(sr => sr.Make)
            .NotEmpty()
            .WithMessage("MakeRequired");

        RuleFor(sr => sr.Model)
            .NotEmpty()
            .WithMessage("ModelRequired");

        RuleFor(sr => sr.ProblemDescription)
            .NotEmpty()
            .WithMessage("ProblemDescriptionRequired");

        RuleFor(sr => sr.CarPlate)
            .NotEmpty()
            .WithMessage("CarPlateRequired");

        RuleFor(sr => sr.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .When(sr => sr.Year.HasValue)
            .WithMessage("InvalidYear");

        RuleFor(sr => sr.PreferredDate)
            .NotEmpty()
            .WithMessage("PreferredDateRequired")
            .GreaterThan(DateTime.Now)
            .WithMessage("PreferredDateMustBeInFuture");

        RuleFor(sr => sr.Email)
            .NotEmpty()
            .WithMessage("EmailRequired")
            .EmailAddress()
            .WithMessage("InvalidEmailFormat");

        RuleFor(sr => sr.AutoRepairId)
            .NotEmpty()
            .WithMessage("AutoRepairIdRequired");
    }
}