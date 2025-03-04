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

public class BarberService(IWebHostEnvironment webHostEnvironment,IBarberRepository barberService ,AppDbContext appDbContext , IValidator<BarberCreate> validator ,AppDbContext context, IMapper mapper)
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

    public async ValueTask<Domain.Entities.Barber?> GetByIdAsync(Guid id, QueryOptions options, CancellationToken cancellationToken)
    {
        return await context.Barbers
            .Include(b => b.Bookings) // Booking'larni qo'shish
            .Include(b=>b.Images)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }


    public  async ValueTask<Domain.Entities.Barber> CreateAsync(BarberCreate product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator
            .ValidateAsync(product,
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()), cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);


        var newuser = await appDbContext.Barbers.FirstOrDefaultAsync(b => b.PhoneNumber == product.PhoneNumber, cancellationToken: cancellationToken);
        if(newuser != null)
        {
            throw new InvalidOperationException("This number or name has already been registered.");
        }
        
        
        var extention = new MethodExtention(webHostEnvironment);
        var imageUrl =  await extention.AddPictureAndGetPath(product.ImageUrl);
        
        var barber = mapper.Map<Domain.Entities.Barber>(product);
        barber.ImageUrl = imageUrl;
        
        barber.CreatedTime = DateTimeOffset.UtcNow;
        return await barberService.CreateAsync(barber, cancellationToken: cancellationToken);
    }

    public async ValueTask<Domain.Entities.Barber> UpdateAsync(BarberDto product,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var barberid = await barberService.GetByIdAsync(product.Id, cancellationToken: cancellationToken);
        if (barberid is null)
            throw new ValidationException("Barber not found");
        
        var extention = new MethodExtention(webHostEnvironment);
        var imageUrl =  await extention.AddPictureAndGetPath(product.ImagetUrl);
        var barber = mapper.Map<Domain.Entities.Barber>(product);
        barber.ImageUrl = imageUrl;
        
        var newbarber = mapper.Map<Domain.Entities.Barber>(barberid);
        newbarber.ModifiedTime = DateTimeOffset.UtcNow;

        var result = await barberService.UpdateAsync(newbarber, cancellationToken: cancellationToken);
        return result;
    }

    public ValueTask<Domain.Entities.Barber?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return barberService.DeleteByIdAsync(productId, commandOptions, cancellationToken);
    }
}