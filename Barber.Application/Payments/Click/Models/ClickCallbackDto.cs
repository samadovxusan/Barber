using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Application.Payments.Click.Models;

public class ClickCallbackDto
{
    [FromForm(Name = "click_trans_id")] public long ClickTransId { get; set; }

    [FromForm(Name = "service_id")] public int ServiceId { get; set; }

    [FromForm(Name = "click_paydoc_id")] public long ClickPaydocId { get; set; }

    [FromForm(Name = "merchant_trans_id")]
    [JsonPropertyName("merchant_trans_id")]
    public string MerchantTransId { get; set; } = null!;

    [FromForm(Name = "merchant_prepare_id")]
    [JsonPropertyName("merchant_prepare_id")]
    public int MerchantPrepareId { get; set; }

    [FromForm(Name = "amount")] public decimal Amount { get; set; }

    [FromForm(Name = "action")] public int Action { get; set; }

    [FromForm(Name = "error")] public int Error { get; set; }

    [FromForm(Name = "error_note")]
    [JsonPropertyName("error_note")]
    public string ErrorNote { get; set; } = null!;

    [FromForm(Name = "sign_time")]
    [JsonPropertyName("sign_time")]
    public string SignTime { get; set; } = null!;

    [FromForm(Name = "sign_string")]
    [JsonPropertyName("sign_string")]
    public string SignString { get; set; } = null!;

    [FromForm(Name = "merchant_confirm_id")]
    [JsonPropertyName("merchant_confirm_id")]
    public int MerchantConfirmId { get; set; }
}