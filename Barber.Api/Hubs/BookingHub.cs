using Microsoft.AspNetCore.SignalR;

namespace Barber.Api.Hubs;

public  class BookingHub : Hub
{
    public  async Task ReservePlace( string message)  
    {
        Console.WriteLine(message);
        await Clients.All.SendAsync("ReceiveMessage", message);  
    }  
    
    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnConnectedAsync();
    }

    // Xabar yuborish metodi
    public async Task SendPrivateMessage(ChatMessageDto message)
    {
        // Saqlash DB ga (sizga service kerak bo'ladi)
        // await _chatService.SaveMessage(chatMessage);

        // Faqat 2ta odam ko‘radi (sender va receiver)
        await Clients.Group(message.SenderId).SendAsync("ReceiveMessage", message);
        await Clients.Group(message.ReceiverId).SendAsync("ReceiveMessage", message);
    }
}
