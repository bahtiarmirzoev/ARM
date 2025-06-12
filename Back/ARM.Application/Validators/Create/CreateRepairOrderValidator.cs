using FluentValidation;
using ARM.Core.Dtos.Create;
using static ARM.Core.Constants.ValidationConstants;
using static ARM.Common.Utilites.ValidationUtilities;

namespace ARM.Application.Validators.Create;

public class CreateRepairOrderValidator : AbstractValidator<CreateRepairOrderDto>
{
    public CreateRepairOrderValidator()
    {
        RuleFor(r => r.CarId)
            .NotEmpty()
            .WithMessage("CarIdRequired");

        RuleFor(r => r.AutoServiceId)
            .NotEmpty()
            .WithMessage("AutoServiceIdRequired");

        RuleFor(r => r.ScheduledDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("ScheduledDateInvalid");

        RuleFor(r => r.EstimatedDuration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("EstimatedDurationInvalid");

        RuleFor(r => r.EstimatedCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ActualCostInvalid");

        RuleFor(r => r.EstimatedCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("EstimatedCostInvalid");

        RuleFor(r => r.CustomerComments)
            .MaximumLength(MaxCommentLength)
            .WithMessage("CustomerCommentsMaxLength");
    }
}