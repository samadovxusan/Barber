using AutoMapper;
using Barber.Application.Barbers.Madels;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Barbers.Mappers;

public class BarberMapper:Profile
{
    public BarberMapper()
    {
        CreateMap<Domain.Entities.Barber,BarberDto>().ReverseMap();
        CreateMap<Domain.Entities.Barber,BarberCreate>().ReverseMap();
        CreateMap<BarberDailySchedule,BarberWokingTime>().ReverseMap();
    }
    
}