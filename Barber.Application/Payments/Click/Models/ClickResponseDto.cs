using System.Text.Json.Serialization;

namespace Barber.Application.Payments.Click.Models;

public class ClickResponseDto
{
    [JsonPropertyName("error")] public int Error { get; set; }

    [JsonPropertyName("error_note")] public string ErrorNote { get; set; } = null!;

    [JsonPropertyName("click_trans_id")] public long ClickTransId { get; set; }

    [JsonPropertyName("merchant_trans_id")]
    public string MerchantTransId { get; set; } = null!;

    [JsonPropertyName("merchant_prepare_id")]
    public int MerchantPrepareId { get; set; }

    [JsonPropertyName("merchant_confirm_id")]
    public int? MerchantConfirmId { get; set; }
}