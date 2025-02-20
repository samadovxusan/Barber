using Barber.Application.Servises.Models;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Application.Servises.Queries;

public class ServiceGetQuery:IQuery<List<Service>>
{
    public ServiceFilters? Filters { set; get; }
}