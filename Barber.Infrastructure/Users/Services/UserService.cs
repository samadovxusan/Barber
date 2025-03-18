using System.Linq.Expressions;
using Barber.Application.Users.Models;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Barber.Infrastructure.Users.Services;

public class UserService(IUserRepository userRepository,AppDbContext context, IValidator<User> validator) : IUserService
{
    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default)
        => userRepository.Get(predicate, queryOptions);

    public IQueryable<User> Get(UserFilter clientFilter, QueryOptions queryOptions = default)
        => userRepository.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);

    public ValueTask<User?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
        => userRepository.GetByIdAsync(clientId, queryOptions, cancellationToken);

    public ValueTask<User> CreateAsync(User user,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(user);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.IsValid.ToString());

        return userRepository.CreateAsync(user, new CommandOptions(skipSaveChanges: false), cancellationToken);
    }

    public async Task<bool> ChangPasswordAsync(ChangePasswordUser changPassword, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var barber = await context.Users.FindAsync(changPassword.Id, cancellationToken);
        var result = barber != null && PasswordHelper.VerifyPassword(barber.Password, changPassword.Password);

        if (result)
        {
            if (barber != null) barber.Password = PasswordHelper.HashPassword(changPassword.NewPassword);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }


    public async ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var foundClient = await GetByIdAsync(user.Id, cancellationToken: cancellationToken) ??
                          throw new InvalidOperationException();

        foundClient.FullName = user.FullName;
        foundClient.Password = user.Password;
        foundClient.PhoneNumber = user.PhoneNumber;
        foundClient.CreatedTime = DateTimeOffset.UtcNow;
        foundClient.Password = PasswordHelper.HashPassword(foundClient.Password);

        return await userRepository.UpdateAsync(foundClient, commandOptions, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => userRepository.DeleteByIdAsync(clientId, commandOptions, cancellationToken);
}