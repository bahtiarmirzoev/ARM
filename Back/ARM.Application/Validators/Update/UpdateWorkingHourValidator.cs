using FluentValidation;
using ARM.Core.Dtos.Update;

namespace ARM.Application.Validators.Update;

public class UpdateWorkingHourValidator : AbstractValidator<UpdateWorkingHourDto>
{
    public UpdateWorkingHourValidator()
    {
        RuleFor(wh => wh.DayOfWeek)
            .NotEmpty()
            .WithMessage("DayOfWeekRequired");

        RuleFor(wh => wh.OpenTime)
            .NotEmpty()
            .WithMessage("StartTimeRequired");

        RuleFor(wh => wh.CloseTime)
            .NotEmpty()
            .WithMessage("EndTimeRequired")
            .GreaterThan(wh => wh.OpenTime)
            .WithMessage("EndTimeMustBeAfterStartTime");
    }
}