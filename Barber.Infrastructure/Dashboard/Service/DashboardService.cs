using Barber.Application.Dashboard;
using Barber.Application.Dashboard.Service;
using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Dashboard.Service;

public class DashboardService(AppDbContext context) : IDashboardService
{
    public async ValueTask<Dictionary<string, int>> GetAllCount()
    {
        Dictionary<string, int> counts = [];

        var count = await context.Users.CountAsync();
        counts.Add("Users", count);


        var admin = context.Users.Where(u => u.Roles == Role.Admin)
            .ToList().Count();


        counts.Add("Admins", admin);

        var barber = context.Users.Where(u => u.Roles == Role.Barber)
            .ToList().Count();


        counts.Add("Barbers", barber);
        return counts;
    }
}