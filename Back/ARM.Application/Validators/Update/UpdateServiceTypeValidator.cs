using FluentValidation;
using ARM.Core.Dtos.Update;

namespace ARM.Application.Validators.Update;

public class UpdateServiceTypeValidator : AbstractValidator<UpdateServiceTypeDto>
{
    public UpdateServiceTypeValidator()
    {
        RuleFor(st => st.Name)
            .NotEmpty()
            .WithMessage("NameRequired")
            .MaximumLength(100)
            .WithMessage("NameTooLong");

        RuleFor(st => st.Description)
            .NotEmpty()
            .WithMessage("DescriptionRequired")
            .MaximumLength(500)
            .WithMessage("DescriptionTooLong");

        RuleFor(st => st.Category)
            .NotEmpty()
            .WithMessage("CategoryRequired")
            .MaximumLength(50)
            .WithMessage("CategoryTooLong");
    }
}