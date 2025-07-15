namespace Barber.Application.Dashboard.Service;

public interface IDashboardService
{
    ValueTask<Dictionary<string, int>> GetAllCount();
}