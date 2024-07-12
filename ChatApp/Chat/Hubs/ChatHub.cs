using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs {
    public class ChatHub : Hub {
        private readonly IChatService _chatService;
        public ChatHub(IChatService chatService) {
            _chatService = chatService;
        }

        public async Task SendMessage(int chatId, string userId, string message) {
            var chat = await _chatService.GetChatById(chatId);
            if (chat != null) {
                var newMessage = new MessageModel {
                    chatId = chatId,
                    User = userId,
                    Text = message,
                    Time = DateTime.UtcNow
                };
                chat.Messages.Add(newMessage);
                await _chatService.UpdateChat(chat);

                await Clients.Group(chatId.ToString()).SendAsync("ReciveMessage", userId, message);
            }
        }

        public async Task JoinChat(int chatId) {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
            await Clients.Group(chatId.ToString()).SendAsync("UserJoined", Context.ConnectionId);

        }

        public async Task LeaveChat(int chatId) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
            await Clients.Group(chatId.ToString()).SendAsync("UserLeft", Context.ConnectionId);

        }
    }
}
