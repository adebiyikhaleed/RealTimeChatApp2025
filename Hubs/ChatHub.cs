using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace RealTimeChatApp2025.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var timestamp = DateTime.UtcNow;
            await Clients.All.SendAsync("ReceiveMessage", user, message, timestamp);
        }
    }
}
