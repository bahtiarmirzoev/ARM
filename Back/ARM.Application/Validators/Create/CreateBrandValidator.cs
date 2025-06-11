using ARM.Core.Dtos.Create;
using FluentValidation;
using static ARM.Core.Constants.ValidationConstants;
using static ARM.Common.Utilites.ValidationUtilities;

namespace ARM.Application.Validators.Create;

public class CreateBrandValidator : AbstractValidator<CreateBrandDto>
{
    public CreateBrandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("BrandNameRequired")
            .Must(IsValidName)
            .WithMessage("BrandNameInvalid")
            .MaximumLength(MaxNameLength)
            .WithMessage("BrandNameMaxLength");

        RuleFor(a => a.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneRequired")
            .Must(IsValidPhone)
            .WithMessage("PhoneInvalid");

        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("EmailRequired")
            .Must(IsValidEmail)
            .WithMessage("EmailInvalid")
            .MaximumLength(MaxEmailLength)
            .WithMessage("EmailMaxLength");

        RuleFor(a => a.Address)
            .NotEmpty()
            .WithMessage("AddressRequired")
            .Must(IsValidAddress)
            .WithMessage("AddressInvalid")
            .MaximumLength(MaxAddressLength)
            .WithMessage("AddressMaxLength");

        RuleFor(a => a.TaxNumber)
            .NotEmpty()
            .WithMessage("TaxNumberRequired")
            .Must(IsValidTaxNumber)
            .WithMessage("TaxNumberInvalid")
            .MaximumLength(MaxTaxNumberLength)
            .WithMessage("TaxNumberMaxLength");

        RuleFor(a => a.BankAccount)
            .NotEmpty()
            .WithMessage("BankAccountRequired")
            .Must(IsValidBankAccount)
            .WithMessage("BankAccountInvalid")
            .MaximumLength(MaxBankAccountLength)
            .WithMessage("BankAccountMaxLength");
    }
}