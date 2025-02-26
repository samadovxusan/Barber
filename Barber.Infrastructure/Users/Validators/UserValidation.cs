using Barber.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;
using Xunarmand.Application.Common.Settings;
using Xunarmand.Domain.Enums;

namespace Barber.Infrastructure.Users.Validators;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;

        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(client => client.FullName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern);


                RuleFor(client => client.Password).NotEmpty();
            }
        );

        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(client => client.FullName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern);


                RuleFor(client => client.Password).NotEmpty();
            }
        );
    }
}