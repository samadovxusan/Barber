using Barber.Domain.Common.Queries;

namespace Barber.Application.Servises.Models;

public class ServiceFilters:FilterPagination
{
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }
    /// <summary>
    /// Overrides base Equals method
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => 
        obj is ServiceFilters clientFilter 
        && clientFilter.GetHashCode() == GetHashCode();
    
}