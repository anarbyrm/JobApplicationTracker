using FluentValidation;
using JobApplicationTracker.Application.Models;

namespace JobApplicationTracker.Application.Validators
{
    public class JobApplicationCreateValidator : AbstractValidator<JobApplicationCreateModel>
    {
        public JobApplicationCreateValidator()
        {
            RuleFor(ja => ja.CompanyName)
                .NotNull().WithMessage("`Company Name` field should not be null")
                .NotEmpty().WithMessage("`Company Name` field should not be empty")
                .Length(3, 125).WithMessage("`Company Name` must be between 3 and 125 character length");
            RuleFor(ja => ja.Position)
                .NotNull().WithMessage("`Position` field should not be null")
                .NotEmpty().WithMessage("`Position` field should not be empty")
                .Length(3, 125).WithMessage("`Position` must be between 3 and 125 character length");
            RuleFor(ja => ja.Note)
                .NotNull().WithMessage("`Note` field should not be null")
                .NotEmpty().WithMessage("`Note` field should not be empty")
                .Length(3, 500).WithMessage("`Note` must be between 3 and 500 character length");
            RuleFor(ja => ja.AppliedAt)
                .NotNull().WithMessage("`Applied at` field should not be null")
                .NotEmpty().WithMessage("`Applied at` field should not be empty")
                .Must(date => !date.Equals(default(DateTime)))
                .WithMessage("Specified value is not a valid date");
            RuleFor(ja => ja.Status)
                .NotNull().WithMessage("`Status` field should not be null")
                .NotEmpty().WithMessage("`Status` field should not be empty")
                .IsInEnum().WithMessage("Specified value is not a valid Application Status");
        }
    }
}
