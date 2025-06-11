using FluentValidation;
using ARM.Core.Dtos.Update;
using static ARM.Core.Constants.ValidationConstants;
using static ARM.Common.Utilites.ValidationUtilities;

namespace ARM.Application.Validators.Update;

public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewValidator()
    {
        RuleFor(r => r.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("RatingInvalid");

        RuleFor(r => r.Comment)
            .Must(IsValidComment)
            .MaximumLength(MaxCommentLength)
            .WithMessage("CommentMaxLength")
            .When(r => !string.IsNullOrEmpty(r.Comment));
    }
}
