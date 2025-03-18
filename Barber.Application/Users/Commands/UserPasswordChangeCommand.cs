using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Users.Commands;

public class UserPasswordChangeCommand:ICommand<bool>
{
    public ChangePasswordUser User{ get; set; } =  new ChangePasswordUser();
    
}