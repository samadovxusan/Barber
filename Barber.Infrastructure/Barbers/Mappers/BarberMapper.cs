using AutoMapper;
using Barber.Application.Barbers.Madels;

namespace Barber.Infrastructure.Barbers.Mappers;

public class BarberMapper:Profile
{
    public BarberMapper()
    {
        CreateMap<Domain.Entities.Barber,BarberDto>().ReverseMap();
        CreateMap<Domain.Entities.Barber,BarberCreate>().ReverseMap();
    }
    
}