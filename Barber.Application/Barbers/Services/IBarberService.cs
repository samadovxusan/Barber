using System.Linq.Expressions;
using Barber.Application.Barbers.Madels;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Application.Barbers.Services;

public interface IBarberService
{
    IQueryable<Domain.Entities.Barber> Get(Expression<Func<Domain.Entities.Barber, bool>>? predicate = null,
        QueryOptions queryOptions = default);

    IQueryable<Domain.Entities.Barber> Get(BarberFilter productFilter, QueryOptions queryOptions = default);

    ValueTask<Domain.Entities.Barber?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);
    ValueTask<BarberInfo> GetBarberInfoAsync(Guid barberId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Domain.Entities.Barber> CreateAsync(BarberCreate product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
    Task<bool> ChangPasswordAsync(ChangPassword changPassword,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
    
    ValueTask SetDailyScheduleAsync( BarberDailySchedule barberDailySchedule,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Domain.Entities.Barber> UpdateAsync(BarberDto product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask GenerateDailyScheduleAsync();

    ValueTask<Domain.Entities.Barber?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}