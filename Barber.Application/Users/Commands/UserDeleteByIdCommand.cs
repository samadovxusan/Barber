using Barber.Domain.Common.Commands;

namespace Barber.Application.Users.Commands;

public record UserDeleteByIdCommand:ICommand<bool>
{
    public Guid UserId { get; set; }
}