using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Barber.Application.Payments.Click.Models;
using Barber.Application.Payments.Click.Service;
using Barber.Domain.Entities;
using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Barber.Infrastructure.Payments.Click.Service;

public class PaymentService(IOptions<ClickSettings> options, AppDbContext context, ILogger<PaymentService> _logger)
    : IPaymentService
{
    private readonly ClickSettings _clickSettings = options.Value;

    public async ValueTask<string> CreatePaymentUrlAsync(CreatePayment dto)
    {
        try
        {
            var subscription = await context.Subscriptions.FindAsync(dto.SubscriptionId);

            if (subscription == null)
            {
                throw new Exception("Subscription not found");
            }

            var finalAmount = subscription.Price * (1 - subscription.DiscountPercentage / 100);
            var roundedAmount = Math.Round((decimal)finalAmount!);

            var payment = new Barber.Domain.Entities.Payments
            {
                UserId = dto.UserProfileId,
                SubscriptionId = dto.SubscriptionId,
                Amount = roundedAmount,
                PaymentProvider = "Click",
                PaymentMethod = "Card",
                Currency = "UZS",
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync();


            var paymentUrl = $"{_clickSettings.BaseUrl}?" +
                             $"service_id={_clickSettings.ServiceId}&" +
                             $"merchant_id={_clickSettings.MerchantId}&" +
                             $"merchant_user_id={_clickSettings.MerchantUserId}&" +
                             $"amount={roundedAmount.ToString("F2", CultureInfo.InvariantCulture)}&" +
                             $"transaction_param={payment.PaymentId}&" +
                             $"merchant_trans_id={payment.TransactionId}&" +
                             $"return_url={Uri.EscapeDataString(_clickSettings.CallbackUrl)}";

            _logger.LogInformation("Click payment URL generated: {Url}", paymentUrl);

            return paymentUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in CreatePaymentUrlAsync");
            throw;
        }
    }

    public async ValueTask<ClickResponseDto> HandleCallbackAsync(ClickCallbackDto request)
    {
        try
        {
            if (request == null)
            {
                return new ClickResponseDto
                {
                    Error = -8,
                    ErrorNote = "Request is null",
                    ClickTransId = 0,
                    MerchantTransId = "",
                    MerchantPrepareId = 0,
                    MerchantConfirmId = 0
                };
            }

            var clickTransId = request.ClickTransId;
            var merchantTransId = request.MerchantTransId;
            var amount = request.Amount;
            var action = request.Action;
            var signTime = request.SignTime;
            var signString = request.SignString;
            var merchantPrepareId = request.MerchantPrepareId;


            _logger.LogInformation("Click callback received with data: " +
                                   "ClickTransId: {ClickTransId}, " +
                                   "ServiceId: {ServiceId}, " +
                                   "ClickPaydocId: {ClickPaydocId}, " +
                                   "MerchantTransId: {MerchantTransId}, " +
                                   "Amount: {Amount}, " +
                                   "Action: {Action}, " +
                                   "Error: {Error}, " +
                                   "ErrorNote: {ErrorNote}, " +
                                   "SignTime: {SignTime}, " +
                                   "SignString: {SignString}, " +
                                   "MerchantPrepareId: {MerchantPrepareId}, " +
                                   "MerchantConfirmId: {MerchantConfirmId}",
                request.ClickTransId,
                request.ServiceId,
                request.ClickPaydocId,
                request.MerchantTransId,
                request.Amount,
                request.Action,
                request.Error,
                request.ErrorNote,
                request.SignTime,
                request.SignString,
                request.MerchantPrepareId,
                request.MerchantConfirmId
            );

            // 1. Sign tekshirish

            // var amountFormatted = amount % 1 == 0
            //     ? amount.ToString("0", CultureInfo.InvariantCulture) // 2970
            //     : amount.ToString("0.00", CultureInfo.InvariantCulture);


            // 2. transaction_param => PaymentId bo'lishi kerak
            if (!int.TryParse(merchantTransId, out var paymentId))
            {
                _logger.LogInformation($"Click callback received at Time. Data: {paymentId}");

                return new ClickResponseDto
                {
                    Error = -4,
                    ErrorNote = "Invalid transaction_param",
                    ClickTransId = clickTransId,
                    MerchantTransId = merchantTransId,
                    MerchantPrepareId = 0,
                    MerchantConfirmId = 0
                };
            }

            // 3. Payment topish
            var payment = await context.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
            _logger.LogInformation($"Click callback not fount paymentiiiid {paymentId}");

            _logger.LogInformation($"Click callback not fount paymentiiiid {payment}");

            if (payment == null)
            {
                _logger.LogInformation($"Click callback not fount paymentiiiid {paymentId}");
                return new ClickResponseDto
                {
                    Error = -5,
                    ErrorNote = "Payment not found",
                    ClickTransId = clickTransId,
                    MerchantTransId = merchantTransId,
                    MerchantPrepareId = 0,
                    MerchantConfirmId = 0
                };
            }

            var subscription = await context.Subscriptions.FindAsync(payment.SubscriptionId);
            var user = await context.Barbers.FindAsync(payment.UserId);

            if (subscription == null || user == null)
            {
                _logger.LogInformation($"Click callback not fount useriiiid {payment.UserId}");
                return new ClickResponseDto
                {
                    Error = -2,
                    ErrorNote = "User or subscription not found",
                    ClickTransId = clickTransId,
                    MerchantTransId = merchantTransId,
                    MerchantPrepareId = 0,
                    MerchantConfirmId = 0
                };
            }

            // 4. Narxni tekshirish
            var expectedAmount = Math.Round((decimal)(subscription.Price * (1 - (subscription.DiscountPercentage??0) / 100)),
                2);

            if (Math.Round(amount, 2) != expectedAmount)
            {
                _logger.LogInformation($"Click callback not fount Priceeee {subscription.Price}");
                return new ClickResponseDto
                {
                    Error = -3,
                    ErrorNote = "Amount mismatch",
                    ClickTransId = clickTransId,
                    MerchantTransId = merchantTransId,
                    MerchantPrepareId = 0,
                    MerchantConfirmId = 0
                };
            }

            var raw = "";
            if (action == 0)
            {
                raw =
                    $"{clickTransId}{request.ServiceId}{_clickSettings.SecretKey}{merchantTransId}{amount}{action}{signTime}";
            }
            else if (action == 1)
            {
                raw =
                    $"{clickTransId}{request.ServiceId}{_clickSettings.SecretKey}{merchantTransId}{paymentId}{amount}{action}{signTime}";
            }

            var generatedSign = CalculateMd5(raw).ToLower();


            if (!string.Equals(generatedSign, signString, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation($"RECEIVED SIGN   xatoooo: {action}", signString);
                return new ClickResponseDto
                {
                    Error = -1,
                    ErrorNote = "Invalid sign string",
                    ClickTransId = clickTransId,
                    MerchantTransId = merchantTransId,
                    MerchantPrepareId = 0,
                    MerchantConfirmId = 0
                };
            }

            // 5. Action handle
            if (action == 0) // Prepare
            {
                _logger.LogInformation($"Prepare successful {clickTransId}");
                return new ClickResponseDto
                {
                    Error = 0,
                    ErrorNote = "Prepare successful",
                    ClickTransId = clickTransId,
                    MerchantTransId = merchantTransId,
                    MerchantPrepareId = payment.PaymentId,
                    MerchantConfirmId = 0
                };
            }

            if (action == 1) // Complete
            {
                _logger.LogInformation($"Complete successful {clickTransId} {payment.PaymentId}");
                if (payment.Status == PaymentStatus.Completed)
                {
                    _logger.LogInformation($"Already paid  userid :{payment.UserId} paymentId :{payment.PaymentId}  ");
                    return new ClickResponseDto
                    {
                        Error = -6,
                        ErrorNote = "Already paid",
                        ClickTransId = clickTransId,
                        MerchantTransId = merchantTransId,
                        MerchantPrepareId = payment.PaymentId,
                        MerchantConfirmId = 0
                    };
                }

                payment.Status = PaymentStatus.Completed;
                payment.ProcessedAt = DateTime.UtcNow;

                var userSubscription = new UserSubscription
                {
                    UserProfileId = user.Id,
                    SubscriptionId = subscription.Id,
                    SubscribedAt = DateTime.UtcNow,
                    ExpireAt = DateTime.UtcNow.AddDays(subscription.DurationInDays),
                    Status = SubscriptionStatus.Active
                };

                context.UserSubscriptions.Add(userSubscription);
                user.IsPremium = true;

                await context.SaveChangesAsync();

                return new ClickResponseDto
                {
                    Error = 0,
                    ErrorNote = "Payment completed successfully",
                    ClickTransId = clickTransId,
                    MerchantTransId = merchantTransId,
                    MerchantPrepareId = 0,
                    MerchantConfirmId = payment.PaymentId
                };
            }

            return new ClickResponseDto
            {
                Error = -8,
                ErrorNote = "Unknown action",
                ClickTransId = clickTransId,
                MerchantTransId = merchantTransId,
                MerchantPrepareId = 0,
                MerchantConfirmId = 0
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, " Xatoooooo  Exception in HandleCallbackAsync");

            return new ClickResponseDto
            {
                Error = -9,
                ErrorNote = "Internal server error",
                ClickTransId = 0,
                MerchantTransId = "",
                MerchantPrepareId = 0,
                MerchantConfirmId = 0
            };
        }
    }

    private static string CalculateMd5(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}