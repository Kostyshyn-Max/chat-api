using AutoMapper;
using ChatApp.BusinessLogic.Interfaces;
using ChatApp.DataAccess.Interfaces;
using ChatApp.Shared.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BusinessLogic.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
    public MessageService(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MessageModel>> GetAllChatMessagesAsync(Guid chatId)
    {
        var messages = await _messageRepository.GetAllAsync(msg => msg.ChatId.Equals(chatId));
        var mappedMessages = messages.Select(_mapper.Map<MessageModel>);
        return mappedMessages;
    }

    public async Task<MessageModel?> GetMessageByIdAsync(Guid messageId)
    {
        var message = await _messageRepository.GetByIdAsync(messageId);
        if (message is null)
        {
            return null;
        }

        return _mapper.Map<MessageModel>(message);
    }

    public async Task<Guid> SaveMessageAsync(MessageModel message)
    {
        var messageId = await _messageRepository.CreateAsync(_mapper.Map<DataAccess.Entities.Message>(message));
        return messageId;
    }
}
