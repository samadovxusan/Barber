using Microsoft.AspNetCore.SignalR;

namespace Barber.Api.Hubs;

public  class BookingHub : Hub
{
    public  async Task ReservePlace( string message)  
    {
        Console.WriteLine(message);
        await Clients.All.SendAsync("ReceiveMessage", message);  
    }  
}
