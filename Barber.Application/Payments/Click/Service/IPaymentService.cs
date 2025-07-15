using Barber.Application.Payments.Click.Models;

namespace Barber.Application.Payments.Click.Service;

public interface IPaymentService
{
    ValueTask<string> CreatePaymentUrlAsync(CreatePayment dto);
    ValueTask<ClickResponseDto> HandleCallbackAsync(ClickCallbackDto dto);
}