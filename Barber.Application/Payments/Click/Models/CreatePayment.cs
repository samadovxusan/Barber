namespace Barber.Application.Payments.Click.Models;

public class CreatePayment
{
    public Guid SubscriptionId { get; set; }
    public Guid UserProfileId { get; set; }
}