using AutoMapper;
using Barber.Application.Location.Models;

namespace Barber.Infrastructure.Location.Mappers;

public class LocationMapper:Profile
{
    public LocationMapper()
    {
        CreateMap<Domain.Entities.Location, LocationDto>();
    }
}