using FluentValidation;
using ARM.Core.Dtos.Update;
using static ARM.Core.Constants.ValidationConstants;
using static ARM.Common.Utilites.ValidationUtilities;

namespace ARM.Application.Validators.Update;

public class UpdateCarValidator : AbstractValidator<UpdateCarDto>
{
    public UpdateCarValidator()
    {
        RuleFor(c => c.Make)
            .MaximumLength(MaxCarMakeLength)
            .WithMessage("CarMakeMaxLength");

        RuleFor(c => c.Model)
            .MaximumLength(MaxCarModelLength)
            .WithMessage("CarModelMaxLength");

        RuleFor(c => c.CarPlate)
            .Must(IsValidCarPlate)
            .WithMessage("CarPlateInvalid")
            .MaximumLength(MaxCarPlateLength)
            .WithMessage("CarPlateMaxLength");

        RuleFor(c => c.Year)
            .InclusiveBetween(MinYear, DateTime.UtcNow.Year)
            .WithMessage("CarYearInvalid");

        RuleFor(c => c.Color)
            .MaximumLength(MaxCarColorLength)
            .WithMessage("CarColorMaxLength");

        RuleFor(c => c.VIN)
            .Matches(VINRegex)
            .WithMessage("CarVINInvalid")
            .MaximumLength(MaxVINLength)
            .WithMessage("CarVINMaxLength");

        RuleFor(c => c.EngineType)
            .MaximumLength(MaxEngineTypeLength)
            .WithMessage("CarEngineTypeMaxLength");

        RuleFor(c => c.EngineVolume)
            .GreaterThan(0)
            .WithMessage("CarEngineVolumeInvalid");

        RuleFor(c => c.Transmission)
            .MaximumLength(MaxTransmissionLength)
            .WithMessage("CarTransmissionMaxLength");
    }
}
