using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using BlazorSignalRApp.Hubs;

namespace BlazorSignalRApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatApiController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILogger<ChatApiController> _logger;

        public ChatApiController(IHubContext<ChatHub> hubContext, ILogger<ChatApiController> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Text);
            _logger.LogInformation("-{Time} - {User} : {Message}" , DateTime.Now, message.User,message.Text);
            return Ok(new { status = "......" });
        }
    }

    public class ChatMessage
    {
        public required string User { get; set; }
        public required string Text { get; set; }
    }
}
