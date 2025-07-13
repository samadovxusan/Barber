using System.Text.Json.Serialization;
using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class Payments : AuditableEntity
{
    public int PaymentId { get; set; }
    public required Guid UserId { get; set; } // Kim to'lov qilgan
    public Guid SubscriptionId { get; set; } // Qaysi tarif uchun to‘lov qilingan

    public string PaymentProvider { get; set; } = null!; // "Click", "Payme", etc.
    public string PaymentMethod { get; set; } = null!; // "Card", "UZCARD", etc.
    public int TransactionId { get; set; } // Click tomonidan berilgan ID
    public decimal Amount { get; set; } // To‘lov summasi
    public string Currency { get; set; } = "UZS"; // Valyuta
    public PaymentStatus Status { get; set; } // Pending, Completed, Failed
    public string? FailureReason { get; set; } // Agar muammo bo‘lsa

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; } // Qachon yakunlandi

    [JsonIgnore] public Barber Barber { get; set; } = null!;
    [JsonIgnore] public Subscription Subscription { get; set; } = null!;
}