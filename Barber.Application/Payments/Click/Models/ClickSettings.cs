namespace Barber.Application.Payments.Click.Models;

public class ClickSettings
{
    public string MerchantId { get; set; } = null!;
    public string MerchantUserId { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string BaseUrl { get; set; } = null!;
    public string CallbackUrl { get; set; } = null!;
}