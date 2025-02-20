using AutoMapper;
using Barber.Application.Servises.Models;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Servises.Mappers;

public class ServiceMapper:Profile
{
    public ServiceMapper()
    {
        CreateMap<Service, ServiceCreate>().ReverseMap();
    }
    
}