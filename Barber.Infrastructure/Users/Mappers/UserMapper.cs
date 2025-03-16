using AutoMapper;
using Barber.Application.Users.Models;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Users.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserCreate, User>().ReverseMap();
        CreateMap<UserCreate, BarbersCreate>().ReverseMap();
        CreateMap<UserCreate, AdminCreate>().ReverseMap();
    }
}