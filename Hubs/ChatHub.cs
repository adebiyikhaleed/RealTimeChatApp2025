using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp2025.Data;
using RealTimeChatApp2025.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeChatApp2025.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string message)
        {
            var chatMessage = new ChatMessage
            {
                User = user,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message, chatMessage.Timestamp);
        }

        public override async Task OnConnectedAsync()
        {
            // Send the latest 50 messages (you can change the number)
            var messages = _context.ChatMessages
                .OrderBy(m => m.Timestamp)
                .Take(50)
                .ToList();

            foreach (var msg in messages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", msg.User, msg.Message, msg.Timestamp);
            }

            await base.OnConnectedAsync();
        }
    }
}
