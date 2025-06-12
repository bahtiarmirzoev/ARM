using FluentValidation;
using ARM.Core.Dtos.Update;
using static ARM.Core.Constants.ValidationConstants;

namespace ARM.Application.Validators.Update;

public class UpdateRepairOrderValidator : AbstractValidator<UpdateRepairOrderDto>
{
    public UpdateRepairOrderValidator()
    {
        RuleFor(r => r.ScheduledDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("ScheduledDateInvalid");

        RuleFor(r => r.EstimatedDuration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("EstimatedDurationInvalid");

        RuleFor(r => r.ActualCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ActualCostInvalid");

        RuleFor(r => r.EstimatedCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("EstimatedCostInvalid");

        RuleFor(r => r.DiagnosisResults)
            .MaximumLength(MaxDiagnosisResultsLength)
            .WithMessage("DiagnosisResultsMaxLength");

        RuleFor(r => r.CancellationReason)
            .MaximumLength(MaxCancellationReasonLength)
            .WithMessage("CancellationReasonMaxLength");

        RuleFor(r => r.CustomerComments)
            .MaximumLength(MaxCommentLength)
            .WithMessage("CustomerCommentsMaxLength");
    }
}
