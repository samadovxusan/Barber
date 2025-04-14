using Barber.Application.Barbers.Madels;

namespace Barber.Infrastructure.Barbers.Validators;

using FluentValidation;

public class BarberCreateValidator : AbstractValidator<BarberCreate>
{
    public BarberCreateValidator()
    {
        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage("Full Name is required.")
            .MinimumLength(3).WithMessage("Full Name must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Full Name must be at most 50 characters.");

        RuleFor(user => user.Age)
            .GreaterThan(0).WithMessage("Age must be greater than 0.")
            .LessThanOrEqualTo(120).WithMessage("Age must be 120 or less.");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\@\#\$\%\^\&\*\(\)\\+\-]")
            .WithMessage("Password must contain at least one special character.");
    }
}