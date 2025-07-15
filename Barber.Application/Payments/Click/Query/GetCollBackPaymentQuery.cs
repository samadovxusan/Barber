using Barber.Application.Payments.Click.Models;
using Barber.Domain.Common.Queries;

namespace Barber.Application.Payments.Click.Query;

public class GetCollBackPaymentQuery:IQuery<ClickResponseDto>
{
    public ClickCallbackDto Callback { get; set; } = null!;
    
}