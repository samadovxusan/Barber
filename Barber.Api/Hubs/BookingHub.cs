using Microsoft.AspNetCore.SignalR;

namespace Barber.Api.Hubs;

public class BookingHub:Hub
{
    public async Task SendBookingUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveBookingUpdate", message);
    }
}