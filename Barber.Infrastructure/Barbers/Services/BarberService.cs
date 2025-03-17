using System.Linq.Expressions;
using AutoMapper;
using Barber.Api.Extentions;
using Barber.Application.Barbers.Madels;
using Barber.Application.Barbers.Services;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Xunarmand.Domain.Enums;

namespace Barber.Infrastructure.Barbers.Services;

public class BarberService(
    IWebHostEnvironment webHostEnvironment,
    IBarberRepository barberService,
    IValidator<BarberCreate> validator,
    AppDbContext context,
    IMapper mapper)
    : IBarberService
{
    public IQueryable<Domain.Entities.Barber> Get(Expression<Func<Domain.Entities.Barber, bool>>? predicate = default,
        QueryOptions queryOptions = default)
    {
        return barberService.Get(predicate, queryOptions);
    }

    public IQueryable<Domain.Entities.Barber> Get(BarberFilter productFilter, QueryOptions queryOptions = default)
    {
        return barberService.Get(queryOptions: queryOptions).ApplyPagination(productFilter);
    }

    public async ValueTask<Domain.Entities.Barber?> GetByIdAsync(Guid id, QueryOptions options,
        CancellationToken cancellationToken)
    {
        return await context.Barbers
            .Include(b => b.Bookings) // Booking'larni qo'shish
            .Include(b => b.Images)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async ValueTask<BarberInfo> GetBarberInfoAsync(Guid barberId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var barber =
                await context.Barbers.FirstOrDefaultAsync(b => b.Id == barberId, cancellationToken: cancellationToken);
            var result = mapper.Map<BarberInfo>(barber);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async ValueTask<Domain.Entities.Barber> CreateAsync(BarberCreate product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator
            .ValidateAsync(product,
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()), cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var extention = new MethodExtention(webHostEnvironment);
        var imageUrl = await extention.AddPictureAndGetPath(product.ImageUrl);


        var barber = mapper.Map<Domain.Entities.Barber>(product);
        barber.ImageUrl = imageUrl;

        barber.CreatedTime = DateTimeOffset.UtcNow;
        barber.Password = PasswordHelper.HashPassword(barber.Password);
        return await barberService.CreateAsync(barber, cancellationToken: cancellationToken);
    }

    public async Task<bool> ChangPasswordAsync(ChangPassword changPassword,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"2- {Thread.CurrentThread.ManagedThreadId} {Thread.CurrentThread.Name}");
        var barber = await context.Barbers.FirstOrDefaultAsync(b => b.Id == changPassword.Id, cancellationToken);
        var result = barber != null && PasswordHelper.VerifyPassword(barber.Password, changPassword.Password);

        if (result)
        {
            if (barber != null) barber.Password = PasswordHelper.HashPassword(changPassword.NewPassword);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        Console.WriteLine($"2- {Thread.CurrentThread.ManagedThreadId} {Thread.CurrentThread.Name}");

        return false;
    }

    public async ValueTask SetDailyScheduleAsync(BarberDailySchedule barberWokingTime,
        CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        if (barberWokingTime.StartTime >= barberWokingTime.EndTime)
            throw new Exception("Boshlanish vaqti tugash vaqtidan oldin bo‘lishi kerak!");

        var existingSchedule =
            await context.BarberDailySchedules.FirstOrDefaultAsync(b => b.BarberId == barberWokingTime.BarberId,
                cancellationToken: cancellationToken);

        if (existingSchedule != null)
        {
            // Agar bor bo‘lsa, yangilaymiz
            existingSchedule.StartTime = barberWokingTime.StartTime;
            existingSchedule.EndTime = barberWokingTime.EndTime;
            existingSchedule.IsWorking = true;
        }
        else
        {
            // Agar yo‘q bo‘lsa, yangi jadval yaratamiz
            var schedule = new BarberDailySchedule
            {
                BarberId = barberWokingTime.BarberId,
                StartTime = barberWokingTime.StartTime,
                EndTime = barberWokingTime.EndTime,
                IsWorking = true
            };
            schedule.CreatedTime = DateTime.UtcNow;
            await context.BarberDailySchedules.AddAsync(schedule, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask<Domain.Entities.Barber> UpdateAsync(BarberDto product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var barberid = await barberService.GetByIdAsync(product.Id, cancellationToken: cancellationToken);
        if (barberid is null)
            throw new ValidationException("Barber not found");

        var extention = new MethodExtention(webHostEnvironment);
        var imageUrl = await extention.AddPictureAndGetPath(product.ImagetUrl);
        var barber = mapper.Map<Domain.Entities.Barber>(product);
        barber.ImageUrl = imageUrl;

        var newbarber = mapper.Map<Domain.Entities.Barber>(barberid);
        newbarber.ModifiedTime = DateTimeOffset.UtcNow;
        newbarber.Password = PasswordHelper.HashPassword(newbarber.Password);

        var result = await barberService.UpdateAsync(newbarber, cancellationToken: cancellationToken);
        return result;
    }

    public async ValueTask GenerateDailyScheduleAsync()
    {
        var today = DateTime.UtcNow.Date;

        var dailySchedules = await context.BarberDailySchedules
            .Where(b => b.IsWorking)
            .ToListAsync();

        foreach (var schedule in dailySchedules)
        {
            var barberSchedule = new BarberDailySchedule()
            {
                BarberId = schedule.BarberId,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                IsWorking = true
            };

            await context.BarberDailySchedules.AddAsync(barberSchedule);
        }

        await context.SaveChangesAsync();
    }

    public ValueTask<Domain.Entities.Barber?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return barberService.DeleteByIdAsync(productId, commandOptions, cancellationToken);
    }
}