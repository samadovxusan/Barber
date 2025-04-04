using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;

namespace Barber.Infrastructure.Barbers.CommandHandlers;

public class CreateBarberCommandHandler(IBarberService barberService,AppDbContext context):ICommandHandler<CreateBerberCommand,bool>
{
    public async Task<bool> Handle(CreateBerberCommand request, CancellationToken cancellationToken)
    {
        var result = await barberService.CreateAsync(request.BarberCreate, cancellationToken: cancellationToken);

        var user = new User()
        {
            Id = result.Id,
            FullName = result.FullName,
            PhoneNumber = result.PhoneNumber,
            Password = result.Password,
            Roles = result.Role,CreatedTime = DateTimeOffset.UtcNow
            
        };
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}