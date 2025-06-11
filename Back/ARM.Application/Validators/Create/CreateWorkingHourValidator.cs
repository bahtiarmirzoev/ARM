using FluentValidation;
using ARM.Core.Dtos.Create;

namespace ARM.Application.Validators.Create;

public class CreateWorkingHourValidator : AbstractValidator<CreateWorkingHourDto>
{
    public CreateWorkingHourValidator()
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