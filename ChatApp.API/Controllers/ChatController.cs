using ChatApp.BusinessLogic.Interfaces;
using ChatApp.Shared.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    [Route("my-chats")]
    [ProducesResponseType<IEnumerable<ChatModel>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ChatModel>>> GetMyChatsAsync()
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var chats = await _chatService.GetAllUserChatsAsync(userId);
        return Ok(chats);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreateChatAsync([FromBody] ChatCreateModel model)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var chatId = await _chatService.CreateChatAsync(userId, model);
        if (chatId is null)
        {
            return BadRequest();
        }

        return Ok(chatId);
    }
}
