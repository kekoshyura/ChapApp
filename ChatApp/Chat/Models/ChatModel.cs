namespace Chat.Models {
    public class ChatModel {
        public int Id { get; set; }
        public string ChatName { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}
