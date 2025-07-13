using System.Text.Json.Serialization;
using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class UserSubscription : AuditableEntity
{
    public Guid UserProfileId { get; set; }
    [JsonIgnore]
    public Barber Barber { get; set; } = null!;

    public Guid SubscriptionId { get; set; }
    [JsonIgnore]
    public Subscription Subscription { get; set; } = null!;

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpireAt { get; set; }

    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;
}