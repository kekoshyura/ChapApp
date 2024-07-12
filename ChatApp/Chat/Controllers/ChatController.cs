using Chat.DTO;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;



namespace Chat.Controllers {
    [Route("api/chatApi")]
    [ApiController]
    public class ChatController : ControllerBase {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService) {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats() {
            return Ok(await _chatService.GetAllChats());
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChat(int id) {
            var chat = await _chatService.GetChatById(id);
            if (chat == null) {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDTO chatDTO) {
            var chat = await _chatService.CreateChat(chatDTO.Name, chatDTO.CreatedBy);
            return CreatedAtAction(nameof(CreateChat), new { chatId = chat.Id }, chat);
        }

        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChat(int id, [FromQuery] string userId) {
            try {
                await _chatService.DeleteChat(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException) {

                return Unauthorized();
            }
        }

        [HttpPut("{chatId}")]
        public async Task<IActionResult> UpdateChatName(int id, [FromBody] UpdateChatDTO dto) {
            var chat = await _chatService.GetChatById(id);
            if (chat == null) {
                return NotFound();
            }
            if (chat.CreatedBy != dto.UserId) {
                return Forbid();
            }

            chat.ChatName = dto.Name;
            await _chatService.UpdateChat(chat);
            return NoContent();
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinChat(string chatId) {
            var connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:44353/chathub")
                    .Build();

            await connection.StartAsync();
            await connection.InvokeAsync("JoinChat", chatId);

            return Ok();
        }

        [HttpPost("disconnect")]
        public async Task<IActionResult> LeavChat(string chatId) {
            var connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:44353/chathub")
                    .Build();

            await connection.StartAsync();
            await connection.InvokeAsync("LeaveChat", chatId);

            await connection.StopAsync();
            await connection.DisposeAsync();

            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(string chatId, string userId, string message) {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://yourapi/chathub")
                .Build();

            await connection.StartAsync();
            await connection.InvokeAsync("SendMessage", chatId, userId, message);

            return Ok();
        }
    }
}
