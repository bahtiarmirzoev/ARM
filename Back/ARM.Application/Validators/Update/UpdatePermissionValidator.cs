using ARM.Core.Dtos.Update;
using FluentValidation;
using static ARM.Core.Constants.ValidationConstants;

namespace ARM.Application.Validators.Update;

public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionDto>
{
    public UpdatePermissionValidator()
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