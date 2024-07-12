using Chat.Models;

namespace Chat.Repository {
    public interface IChatRepository {
        Task<ChatModel> GetById(int id);
        Task<ChatModel> CreateChat(ChatModel chat);
        Task DeleteChat(ChatModel chat);
        Task<IEnumerable<ChatModel>> GetAllChats();
        Task UpdateChat(ChatModel chat);
    }
}
