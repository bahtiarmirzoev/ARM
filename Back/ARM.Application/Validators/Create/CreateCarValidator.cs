using ARM.Core.Dtos.Create;
using FluentValidation;
using static ARM.Core.Constants.ValidationConstants;
using static ARM.Common.Utilites.ValidationUtilities;

namespace ARM.Application.Validators.Create;

public class CreateCarValidator : AbstractValidator<CreateCarDto>
{
    public CreateCarValidator()
    {
        RuleFor(c => c.Make)
            .NotEmpty()
            .WithMessage("CarMakeRequired")
            .MaximumLength(MaxCarMakeLength)
            .WithMessage("CarMakeMaxLength");

        RuleFor(c => c.Model)
            .NotEmpty()
            .WithMessage("CarModelRequired")
            .MaximumLength(MaxCarModelLength)
            .WithMessage("CarModelMaxLength");

        RuleFor(c => c.CarPlate)
            .NotEmpty()
            .WithMessage("CarPlateRequired")
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
            .NotEmpty()
            .WithMessage("CarVINRequired")
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