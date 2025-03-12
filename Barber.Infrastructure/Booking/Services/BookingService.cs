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
        booking.CreatedTime = DateTimeOffset.UtcNow;

        var barberSchedule = await _context.BarberDailySchedules
            .FirstOrDefaultAsync(b => b.BarberId == booking.BarberId
                                      && b.StartTime <= booking.AppointmentTime
                                      && b.EndTime >= booking.AppointmentTime, cancellationToken: cancellationToken);

        if (barberSchedule == null)
            throw new Exception("Barber bu vaqtda ishlamaydi.");

        var existingBooking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BarberId == booking.BarberId
                                      && b.AppointmentTime == booking.AppointmentTime, cancellationToken: cancellationToken);

        if (existingBooking != null)
            throw new Exception("Ushbu vaqt allaqachon band qilingan.");

        // 1. Booking vaqtincha saqlanadi
        booking.Status = Status.Pending;
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        // 2. Barberga API orqali ruxsat so‘rash
        var approvalRequest = new BarberApprovalRequested
        {
            BarberId = booking.BarberId,
            BookingId = booking.Id,
            AppointmentTime = booking.AppointmentTime
        };

        var isApproved = await repositoriess.RequestApprovalAsync(approvalRequest, cancellationToken: cancellationToken);

        if (!isApproved)
        {
            booking.Status = Status.Confirmed;
            await _context.SaveChangesAsync(cancellationToken);
            return false;
        }

        booking.Status = Status.Confirmed;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async ValueTask<bool> RequestApprovalAsync(BarberApprovalRequested request, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:5000/api/barber/approve", request);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var result = await response.Content.ReadFromJsonAsync<BarberApprovalResponse>();
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