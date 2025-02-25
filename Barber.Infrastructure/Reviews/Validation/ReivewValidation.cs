using Barber.Domain.Entities;
using FluentValidation;

namespace Barber.Infrastructure.Reviews.Validation;

public class ReivewValidation : AbstractValidator<Review>
{
    public ReivewValidation()
    {
        RuleFor(r => r.BarberId)
            .NotEmpty().WithMessage("BarberId is required.")
            .GreaterThan(0).WithMessage("BarberId must be greater than 0.");

        RuleFor(r => r.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MinimumLength(5).WithMessage("Comment must be at least 5 characters.")
            .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");

        RuleFor(r => r.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
    }
}