using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Barber.Infrastructure.Barbers.Services;

public class TimeScheduleGenerator : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public TimeScheduleGenerator(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            var nextRun = now.Date.AddDays(1).AddHours(4); // Ertalab soat 4:00 da ishlaydi

            var delay = nextRun - now;
            await Task.Delay(delay, stoppingToken);

            using (var scope = _scopeFactory.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetRequiredService<BarberService>();
                await scheduleService.GenerateDailyScheduleAsync();
            }
        }
    }
}