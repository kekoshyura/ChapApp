using Chat.Models;

namespace Chat.Services {
    public interface IChatService {
        Task<ChatModel> GetChatById(int id);
        Task<ChatModel> CreateChat(string name, string createdBy);
        Task DeleteChat(int id, string userId);
        Task<IEnumerable<ChatModel>> GetAllChats();
        Task UpdateChat(ChatModel chat);
    }
}
