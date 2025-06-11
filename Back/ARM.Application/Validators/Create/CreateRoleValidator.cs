using ARM.Core.Dtos.Create;
using FluentValidation;

namespace ARM.Application.Validators.Create;

public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleValidator()
    {
        RuleFor(role => role.Name)
            .NotEmpty()
            .WithMessage("RoleNameRequired");
    }
}