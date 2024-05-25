using FluentValidation;
using JobApplicationTracker.Application.Models;

namespace JobApplicationTracker.Application.Validators
{
    public class JobApplicationUpdateValidator : AbstractValidator<JobApplicationUpdateModel>
    {
        public JobApplicationUpdateValidator()
        {
            RuleFor(ja => ja.Note)
                .NotNull().WithMessage("`Note` field should not be null")
                .NotEmpty().WithMessage("`Note` field should not be empty")
                .Length(3, 500).WithMessage("`Note` must be between 3 and 500 character length");
            RuleFor(ja => ja.Status)
                .NotNull().WithMessage("`Status` field should not be null")
                .NotEmpty().WithMessage("`Status` field should not be empty")
                .IsInEnum().WithMessage("Specified value is not a valid Application Status");
        }
    }
}
