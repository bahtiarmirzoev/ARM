using ARM.Core.Dtos.Update;
using FluentValidation;
using static ARM.Core.Constants.ValidationConstants;

namespace ARM.Application.Validators.Update;

public class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequestDto>
{
    public UpdateServiceRequestValidator()
    {
        RuleFor(r => r.Status)
            .IsInEnum()
            .WithMessage("ServiceRequestStatusInvalid");

        RuleFor(r => r.ProblemDescription)
            .MaximumLength(MaxProblemDescriptionLength)
            .WithMessage("ProblemDescriptionMaxLength");
    }
}