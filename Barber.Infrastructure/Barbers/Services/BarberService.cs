using System.Linq.Expressions;
using AutoMapper;
using Barber.Application.Barbers.Madels;
using Barber.Application.Barbers.Services;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using FluentValidation;
using Xunarmand.Domain.Enums;

namespace Barber.Infrastructure.Barbers.Services;

public class BarberService(IBarberRepository barberService, IValidator<BarberCreate> validator, IMapper mapper)
    : IBarberService
{
    public IQueryable<Domain.Entities.Barber> Get(Expression<Func<Domain.Entities.Barber, bool>>? predicate = default,
        QueryOptions queryOptions = default)
    {
        return barberService.Get(predicate,queryOptions);
    }

    public IQueryable<Domain.Entities.Barber> Get(BarberFilter productFilter, QueryOptions queryOptions = default)
    {
        return barberService.Get(queryOptions:queryOptions).ApplyPagination(productFilter);
    }

    public ValueTask<Domain.Entities.Barber?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return barberService.GetByIdAsync(userId, queryOptions, cancellationToken);
    }

    public ValueTask<Domain.Entities.Barber> CreateAsync(BarberCreate product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = validator
            .Validate(product,
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var barber = mapper.Map<Domain.Entities.Barber>(product);
        return barberService.CreateAsync(barber);
    }

    public async ValueTask<Domain.Entities.Barber> UpdateAsync(BarberDto product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var barberid = await barberService.GetByIdAsync(product.Id);
        if (barberid is null)
            throw new ValidationException("Barber not found");

        var newbarber = mapper.Map<Domain.Entities.Barber>(barberid);

        var result = await barberService.UpdateAsync(newbarber, cancellationToken: cancellationToken);
        return result;
    }

    public ValueTask<Domain.Entities.Barber?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return barberService.DeleteByIdAsync(productId, commandOptions, cancellationToken);
    }
}