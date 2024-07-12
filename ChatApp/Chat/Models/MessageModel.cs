namespace Chat.Models {
    public class MessageModel {
        public int Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
        public int chatId { get; set; }
        public ChatModel Chat { get; set; }
    }
}
