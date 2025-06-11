using FluentValidation;
using ARM.Core.Dtos.Create;
using static ARM.Core.Constants.ValidationConstants;
using static ARM.Common.Utilites.ValidationUtilities;

namespace ARM.Application.Validators.Create;

public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidator()
    {
        RuleFor(r => r.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("RatingInvalid");

        RuleFor(r => r.Comment)
            .MaximumLength(MaxCommentLength)
            .WithMessage("CommentMaxLength");

        RuleFor(r => r.AutoServiceId)
            .NotEmpty()
            .WithMessage("AutoServiceIdRequired");
    }
}
