using AutoMapper;
using Barber.Application.Users.Models;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Users.Mappers;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}