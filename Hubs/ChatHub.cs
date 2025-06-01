using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTimeChatApp2025.Hubs
{
    public class ChatHub : Hub
    {
        // 🧠 In-memory message storage (shared across users)
        private static List<ChatMessage> _messageHistory = new();

        public async Task SendMessage(string user, string message)
        {
            var timestamp = DateTime.UtcNow;

            var chatMessage = new ChatMessage
            {
                User = user,
                Message = message,
                Timestamp = timestamp
            };

            _messageHistory.Add(chatMessage);

            await Clients.All.SendAsync("ReceiveMessage", user, message, timestamp);
        }

        public override async Task OnConnectedAsync()
        {
            // When a user connects, send them existing chat history
            foreach (var msg in _messageHistory)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", msg.User, msg.Message, msg.Timestamp);
            }

            await base.OnConnectedAsync();
        }
    }

    public class ChatMessage
    {
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
