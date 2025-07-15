using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Barber.Infrastructure.Payments.Click.Service;

public class SubscriptionExpirationChecker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SubscriptionExpirationChecker> _logger;

    public SubscriptionExpirationChecker(IServiceScopeFactory scopeFactory,
        ILogger<SubscriptionExpirationChecker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var now = DateTime.UtcNow;

            var expiredSubs = await db.UserSubscriptions
                .Where(x => x.ExpireAt < now && x.Status == SubscriptionStatus.Active)
                .ToListAsync(stoppingToken);

            foreach (var sub in expiredSubs)
            {
                sub.Status = SubscriptionStatus.Cancelled;

                var user = await db.Barbers.FindAsync(new object[] { sub.UserProfileId },
                    cancellationToken: stoppingToken);
                if (user != null)
                {
                    user.IsPremium = false;
                }
            }
            await db.SaveChangesAsync(stoppingToken);

            _logger.LogInformation("Subscription checker ran at: {Time}. Updated: {Count} users", now,
                expiredSubs.Count);

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}