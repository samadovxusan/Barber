using Barber.Application.Payments.Click.Models;
using Barber.Application.Payments.Click.Query;
using Barber.Application.Payments.Click.Service;
using Barber.Domain.Common.Queries;

namespace Barber.Infrastructure.Payments.Click.QueyHandler;

public class GetCollBackPaymentQueryHandler(IPaymentService service)
    : IQueryHandler<GetCollBackPaymentQuery, ClickResponseDto>
{
    public async Task<ClickResponseDto> Handle(GetCollBackPaymentQuery request, CancellationToken cancellationToken)
    {
        var result = await service.HandleCallbackAsync(request.Callback);
        return result;
    }
}