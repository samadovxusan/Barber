using System.Linq.Expressions;
using AutoMapper;
using Barber.Application.Location.Models;
using Barber.Application.Location.Service;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Location.Sevice;

public class LocationService:ILocationService
{
    private readonly ILocationRepository _repository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;
    public LocationService(ILocationRepository repository, IMapper mapper, AppDbContext dbContext)
    {
        _repository = repository;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public IQueryable<Domain.Entities.Location> Get(Expression<Func<Domain.Entities.Location, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return _repository.Get(predicate, queryOptions);
    }

    public IQueryable<Domain.Entities.Location> Get(UserFilter clientFilter, QueryOptions queryOptions = default)
    {

        return _repository.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);
    }

    public  IQueryable<Domain.Entities.Barber> Get(Guid barberId, QueryOptions queryOptions = default)
    {
        var barber = _dbContext.Barbers.Where(b => b.Id == barberId)
            .Include(b => b.Location);
        return barber;
        
    }

    public ValueTask<Domain.Entities.Location?> GetByIdAsync(Guid locationId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetById(locationId, queryOptions, cancellationToken);
    }

    public ValueTask<Domain.Entities.Location> CreateAsync(LocationDto location, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var map = new Domain.Entities.Location()
        {
            Id = Guid.NewGuid(),
            BarberId = location.BarberId,
            District = location.District,
            Address = location.Address,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Region = location.Region,
            Barber = location.Barber,
            CreatedTime = DateTimeOffset.UtcNow,
        };
        return _repository.Create(map, cancellationToken);
    }

    public async ValueTask<Domain.Entities.Location> UpdateAsync(LocationDto location, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var barberLocation = await _dbContext.Locations
            .FirstOrDefaultAsync(b => b.BarberId == location.BarberId, cancellationToken: cancellationToken);

        if (barberLocation == null)
            return null; 

        barberLocation.District = location.District;
        barberLocation.Address = location.Address;
        barberLocation.Latitude = location.Latitude;
        barberLocation.Longitude = location.Longitude;
        barberLocation.Region = location.Region;
        barberLocation.ModifiedTime = DateTimeOffset.UtcNow;

        var result = await _repository.Update(barberLocation, cancellationToken);
        return result;
    }


}