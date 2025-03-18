namespace Barber.Application.Dashboard;

public interface IDashboardService
{
    ValueTask<Dictionary<string, int>> GetAllCount();
}