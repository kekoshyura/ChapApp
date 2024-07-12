using Chat.Data;
using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repository {
    public class ChatRepository : IChatRepository {

        private readonly ChatDbContext _dbContext;
        public ChatRepository(ChatDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ChatModel> CreateChat(ChatModel chat) {
            _dbContext.Chats.Add(chat);
            await _dbContext.SaveChangesAsync();
            return chat;
        }

        public async Task DeleteChat(ChatModel chat) {
            _dbContext.Chats.Remove(chat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatModel>> GetAllChats() {
            return await _dbContext.Chats.ToListAsync();
        }

        public async Task<ChatModel> GetById(int id) {
            return await _dbContext.Chats.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateChat(ChatModel chat) {
            _dbContext.Chats.Update(chat);
            await _dbContext.SaveChangesAsync();
        }
    }
}
