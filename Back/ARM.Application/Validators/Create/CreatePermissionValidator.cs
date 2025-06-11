using ARM.Core.Dtos.Create;
using FluentValidation;
using static ARM.Core.Constants.ValidationConstants;

namespace ARM.Application.Validators.Create;

public class CreatePermissionValidator : AbstractValidator<CreatePermissionDto>
{
    public CreatePermissionValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("PermissionNameRequired")
            .MaximumLength(MaxPermissionNameLength)
            .WithMessage("PermissionNameMaxLength");

        RuleFor(p => p.Description)
            .MaximumLength(MaxPermissionDescriptionLength)
            .WithMessage("PermissionDescriptionMaxLength");
    }
}