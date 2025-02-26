using System.Linq.Expressions;
using Barber.Application.Users.Models;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Barber.Infrastructure.Users.Services;

public class UserService(IUserRepository userRepository, IValidator<User> validator) : IUserService
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
        var validationResult = validator
            .Validate(user);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.IsValid.ToString());

        return userRepository.CreateAsync(user, new CommandOptions(skipSaveChanges: false), cancellationToken);
    }


    public async ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var foundClient = await GetByIdAsync(user.Id, cancellationToken: cancellationToken) ??
                          throw new InvalidOperationException();

        foundClient.FullName = user.FullName;
        foundClient.Password = user.Password;
        foundClient.PhoneNumber = user.PhoneNumber;

        return await userRepository.UpdateAsync(foundClient, commandOptions, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => userRepository.DeleteByIdAsync(clientId, commandOptions, cancellationToken);
}