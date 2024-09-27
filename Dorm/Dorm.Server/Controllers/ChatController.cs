using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO.Chat;
using Dorm.Server.Hubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IOptions<AuthSettings> _options;

        public ChatController(IChatService chatService, IHubContext<ChatHub> chatHub, IOptions<AuthSettings> options)
        {
            _chatService = chatService;
            _chatHub = chatHub;
            _options = options;
        }


        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages()
        {
            var response = await _chatService.GetAllMessages(0);
            return Ok(response.Data);
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessage(ChatMessageDto model)
        {
            //var token = Request.Cookies["authToken"];

            //if (string.IsNullOrEmpty(token))
            //{
            //    return Unauthorized("Token is missing");
            //}

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(_options.Value.SecretKey);

            //var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(key),
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //}, out SecurityToken validatedToken);

            //var userIdClaim = principal.FindFirst("id")?.Value;

            //model.UserId = int.Parse(userIdClaim);

            var response = await _chatService.SaveMessage(model);
            if (response.Data)
            {
                return Ok();
            }

            return BadRequest(response.Description);
        }

        // Новый метод для удаления сообщения
        [HttpDelete("message/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var response = await _chatService.DeleteMessage(id);
            if (response.Data)
            {
                await _chatHub.Clients.All.SendAsync("DeleteMessage", id);
                return Ok();
            }

            return BadRequest(response.Description);
        }

        // Новый метод для редактирования сообщения
        [HttpPut("message/{id}")]
        public async Task<IActionResult> EditMessage(int id, ChatMessageDto model)
        {
            var response = await _chatService.EditMessage(id, model);
            if (response.Data)
            {
                await _chatHub.Clients.All.SendAsync("UpdateMessage", id, model.Content);
                return Ok();
            }

            return BadRequest(response.Description);
        }
    }
}
