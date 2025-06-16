using ChatApp.BusinessLogic.Interfaces;
using ChatApp.Shared.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }   

    [HttpGet]
    [Route("all/{chatId:guid}")]
    [ProducesResponseType<IEnumerable<MessageModel>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllChatMessagesAsync([FromRoute] Guid chatId)
    {
        var messages = await _messageService.GetAllChatMessagesAsync(chatId);
        return Ok(messages);
    }
}
