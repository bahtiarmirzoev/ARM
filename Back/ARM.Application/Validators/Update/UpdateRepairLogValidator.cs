using FluentValidation;
using ARM.Core.Dtos.Update;
using static ARM.Core.Constants.ValidationConstants;

namespace ARM.Application.Validators.Update;

public class UpdateRepairLogValidator : AbstractValidator<UpdateRepairLogDto>
{
    public UpdateRepairLogValidator()
    {
        RuleFor(r => r.RepairDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("RepairDateInvalid");

        RuleFor(r => r.Description)
            .MaximumLength(MaxRepairDescriptionLength)
            .WithMessage("RepairDescriptionMaxLength");

        RuleFor(r => r.Cost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RepairCostInvalid");

        RuleFor(r => r.Diagnosis)
            .MaximumLength(MaxRepairDiagnosisLength)
            .WithMessage("RepairDiagnosisMaxLength");

        RuleFor(r => r.WorkPerformed)
            .MaximumLength(MaxRepairWorkPerformedLength)
            .WithMessage("RepairWorkPerformedMaxLength");

        RuleFor(r => r.PartsReplaced)
            .MaximumLength(MaxPartsReplacedLength)
            .WithMessage("RepairPartsReplacedMaxLength");

        RuleFor(r => r.Recommendations)
            .MaximumLength(MaxRepairRecommendationsLength)
            .WithMessage("RepairRecommendationsMaxLength");

        RuleFor(r => r.PartsCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RepairPartsCostInvalid");

        RuleFor(r => r.LaborCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RepairLaborCostInvalid");

        RuleFor(r => r.Mileage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RepairMileageInvalid");

        RuleFor(r => r.Services)
            .NotEmpty()
            .WithMessage("RepairServicesRequired");
    }
}
