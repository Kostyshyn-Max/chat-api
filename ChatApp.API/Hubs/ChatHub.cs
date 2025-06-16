using ChatApp.BusinessLogic.Interfaces;
using ChatApp.Shared.Models.Message;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;
    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task SendPrivateMessage(string recipientId, string message, string chatId)
    {
        var senderId = Context.UserIdentifier;
        if (senderId is null)
        {
            return;
        }

        MessageModel messageModel = new()
        {
            ChatId = Guid.Parse(chatId),
            SenderId = Guid.Parse(senderId),
            Content = message,
        };

        var messageId = await _messageService.SaveMessageAsync(messageModel);
        var resultMessage = await _messageService.GetMessageByIdAsync(messageId);

        await Clients.User(senderId).SendAsync("ReceivePrivateMessage", resultMessage);
        await Clients.User(recipientId).SendAsync("ReceivePrivateMessage", resultMessage);
    }
}
