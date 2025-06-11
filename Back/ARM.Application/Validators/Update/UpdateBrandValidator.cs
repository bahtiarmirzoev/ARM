using FluentValidation;
using ARM.Core.Dtos.Update;
using static ARM.Core.Constants.ValidationConstants;
using static ARM.Common.Utilites.ValidationUtilities;

namespace ARM.Application.Validators.Update;

public class UpdateBrandValidator : AbstractValidator<UpdateBrandDto>
{
    public UpdateBrandValidator()
    {
        RuleFor(a => a.Name)
            .Must(IsValidName)
            .WithMessage("BrandNameInvalid")
            .MaximumLength(MaxNameLength)
            .WithMessage("BrandNameMaxLength");

        RuleFor(a => a.Description)
            .MaximumLength(MaxDescriptionLength)
            .WithMessage("BrandDescriptionMaxLength");

        RuleFor(a => a.PhoneNumber)
            .Must(IsValidPhone)
            .WithMessage("PhoneInvalid");

        RuleFor(a => a.Email)
            .Must(IsValidEmail)
            .WithMessage("EmailInvalid")
            .MaximumLength(MaxEmailLength)
            .WithMessage("EmailMaxLength");

        RuleFor(a => a.Address)
            .Must(IsValidAddress)
            .WithMessage("AddressInvalid")
            .MaximumLength(MaxAddressLength)
            .WithMessage("AddressMaxLength");

        RuleFor(a => a.TaxNumber)
            .Must(IsValidTaxNumber)
            .WithMessage("TaxNumberInvalid")
            .MaximumLength(MaxTaxNumberLength)
            .WithMessage("TaxNumberMaxLength");

        RuleFor(a => a.BankAccount)
            .Must(IsValidBankAccount)
            .WithMessage("BankAccountInvalid")
            .MaximumLength(MaxBankAccountLength)
            .WithMessage("BankAccountMaxLength");
    }
}
