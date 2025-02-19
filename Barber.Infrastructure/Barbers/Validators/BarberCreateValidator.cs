using Barber.Application.Barbers.Madels;

namespace Barber.Infrastructure.Barbers.Validators;

using FluentValidation;

public class BarberCreateValidator : AbstractValidator<BarberCreate>
{
    public BarberCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
