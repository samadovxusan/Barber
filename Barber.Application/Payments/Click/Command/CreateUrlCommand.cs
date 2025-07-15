using Barber.Application.Payments.Click.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Payments.Click.Command;

public class CreateUrlCommand:ICommand<string>
{
    public CreatePayment Payment { get; set; } = new CreatePayment();
}