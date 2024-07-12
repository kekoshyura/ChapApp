using Chat.Models;
using Chat.Repository;

namespace Chat.Services {
    public class ChatService : IChatService {

        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository) {
            _chatRepository = chatRepository;
        }
        public async Task<ChatModel> CreateChat(string name, string createdBy) {
            var chat = new ChatModel { ChatName = name, CreatedBy = createdBy };
            return await _chatRepository.CreateChat(chat);
        }

        public async Task DeleteChat(int id, string userId) {
            var chat = await _chatRepository.GetById(id);
            if (chat == null || chat.CreatedBy != userId) {
                throw new ArgumentException("You can't delete this chat");
            }
            await _chatRepository.DeleteChat(chat);
        }

        public async Task<IEnumerable<ChatModel>> GetAllChats() {
            return await _chatRepository.GetAllChats();
        }

        public async Task<ChatModel> GetChatById(int id) {
            return await _chatRepository.GetById(id);
        }

        public async Task UpdateChat(ChatModel chat) {
            await _chatRepository.UpdateChat(chat);
        }
    }
}
