using AutoMapper;
using Barber.Application.Reviews.Models;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Reviews.Mappers;

public class ReivewMapper:Profile
{
    public ReivewMapper()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();
    }
}