using System.Linq.Expressions;
using System.Net.Http.Json;
using Barber.Application.Booking.Models;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Booking.Services;

public class BookingService(IBookingRepositoriess repositoriess,  AppDbContext _context,HttpClient _httpClient) : IBookingService
{
    public IQueryable<Domain.Entities.Booking> Get(Expression<Func<Domain.Entities.Booking, bool>>? predicate = default,
        QueryOptions queryOptions = default)
    {
        return repositoriess.Get(predicate, queryOptions);
    }

    public IQueryable<Domain.Entities.Booking> Get(FilterBooking clientFilter, QueryOptions queryOptions = default)
    {
        return repositoriess.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);
    }

    public ValueTask<Domain.Entities.Booking?> GetByIdAsync(Guid bookingId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return repositoriess.GetById(bookingId, queryOptions, cancellationToken);
    }

    public async ValueTask<List<Domain.Entities.Booking>?> GetByIdBarberAsync(Guid bookingId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var barberBooking = await repositoriess.Get(x => x.BarberId == bookingId)
            .ToListAsync(cancellationToken: cancellationToken);
        return barberBooking;
    }

    public async ValueTask<bool> CreateAsync(Domain.Entities.Booking booking,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            booking.CreatedTime = DateTimeOffset.UtcNow;

            var barberSchedule = await _context.BarberDailySchedules
                .FirstOrDefaultAsync(b => b.BarberId == booking.BarberId
                                          && b.StartTime <= booking.AppointmentTime
                                          && b.EndTime >= booking.AppointmentTime, cancellationToken: cancellationToken);

            if (barberSchedule == null)
                throw new Exception("Barber bu vaqtda ishlamaydi.");

            
            
            
            await _context.Bookings.AddAsync(booking, cancellationToken: cancellationToken);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async ValueTask<bool> ChangeBooking(BarberApprovalRequested request, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (request.Conformetion)
                return true;

        var result = await repositoriess.Delete(request.BookingId,commandOptions, cancellationToken);
        if (result != null)
            return false;
        return false;
    }

    public async ValueTask<bool> RequestApprovalAsync(BarberApprovalRequested request, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("https://95.47.238.221:3034/api/barber/approve", request, cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var result = await response.Content.ReadFromJsonAsync<BarberApprovalResponse>(cancellationToken: cancellationToken);
        return result.IsApproved;
    }

    public ValueTask<Domain.Entities.Booking> UpdateAsync(Domain.Entities.Booking booking,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        booking.ModifiedTime = DateTimeOffset.UtcNow;
        return repositoriess.Update(booking, cancellationToken);
    }

    public ValueTask<Domain.Entities.Booking?> DeleteByIdAsync(Guid serviceId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return repositoriess.Delete(serviceId, commandOptions, cancellationToken);
    }
}