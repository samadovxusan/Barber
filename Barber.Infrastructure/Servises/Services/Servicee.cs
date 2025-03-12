using System.Linq.Expressions;
using System.Net;
using Barber.Api.Extentions;
using Barber.Application.Servises.Models;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Servises.Services;

public class Servicee(IServiceRepository serviceRepository ,IWebHostEnvironment webHostEnvironment) : IService
{
    public IQueryable<Service> Get(Expression<Func<Service, bool>>? predicate = default,
        QueryOptions queryOptions = default)
    {
        return serviceRepository.Get(predicate, queryOptions);
    }

    public IQueryable<Service> Get(ServiceFilters clientFilter, QueryOptions queryOptions = default)
    {
        return serviceRepository.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);
    }

    public ValueTask<Service?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return serviceRepository.GetByIdAsync(userId, queryOptions, cancellationToken);
    }

    public async ValueTask<List<Service>> GetByIdBarberServiceAsync(Guid barberId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var barberService = await serviceRepository.Get(x => x.BarberId == barberId).ToListAsync(cancellationToken: cancellationToken);
        return barberService;

    }

    public async ValueTask<Service> CreateAsync(ServiceCreate service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        
        var extention = new MethodExtention(webHostEnvironment);
        var picturepa = await extention.AddPictureAndGetPath(service.ImageUrl);
        var newservice = new Service
        {
            BarberId = service.BarberId,
            Name = service.Name,
            Duration = service.Duration,
            Price = service.Price,
            CreatedTime = DateTimeOffset.UtcNow,
            ImagerUrl = picturepa
        };
        
        return await serviceRepository.CreateAsync(newservice, commandOptions, cancellationToken);
    }

    public async ValueTask<Service> UpdateAsync(ServiceUpdate service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var extention = new MethodExtention(webHostEnvironment);
        var picturepa = await extention.AddPictureAndGetPath(service.ImageUrl);
        var newservice = new Service
        {
            Id = service.Id,
            BarberId = service.BarberId,
            Name = service.Name,
            Duration = service.Duration,
            Price = service.Price,
            CreatedTime = DateTimeOffset.UtcNow,
            ImagerUrl = picturepa
        };
        return await serviceRepository.UpdateAsync(newservice, commandOptions, cancellationToken);
    }

    public ValueTask<Service?> DeleteByIdAsync(Guid serviceId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return serviceRepository.DeleteByIdAsync(serviceId, commandOptions, cancellationToken);
    }
}