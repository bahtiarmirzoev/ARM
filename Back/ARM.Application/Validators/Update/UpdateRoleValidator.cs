using ARM.Core.Dtos.Update;
using FluentValidation;

namespace ARM.Application.Validators.Update;

public class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleValidator()
    {
        RuleFor(role => role.Name)
            .Must(name => string.IsNullOrWhiteSpace(name) || name.Length > 0)
            .WithMessage("RoleNameRequired");
    }
}