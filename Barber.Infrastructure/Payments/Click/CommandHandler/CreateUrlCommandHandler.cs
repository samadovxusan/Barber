using Barber.Application.Payments.Click.Command;
using Barber.Application.Payments.Click.Service;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Payments.Click.CommandHandler;

public class CreateUrlCommandHandler(IPaymentService service) : ICommandHandler<CreateUrlCommand, string>
{
    public async Task<string> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
    {
        var result = await service.CreatePaymentUrlAsync(request.Payment);
        return result;
    }
}