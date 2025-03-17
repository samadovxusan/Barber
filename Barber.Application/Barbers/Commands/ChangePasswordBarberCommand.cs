using Barber.Application.Barbers.Madels;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Barbers.Commands;

public class ChangePasswordBarberCommand:ICommand<bool>
{
    public ChangPassword ChangPassword { get; set; } = new ChangPassword();
}