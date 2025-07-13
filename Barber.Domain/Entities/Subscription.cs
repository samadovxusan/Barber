using Barber.Domain.Common.Entities;

namespace Barber.Domain.Entities;

public class Subscription : AuditableEntity
{
    public string Name { get; set; } = null!;
    public int DurationInDays { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
    public ICollection<Payments> Payments { get; set; } = new List<Payments>();
}