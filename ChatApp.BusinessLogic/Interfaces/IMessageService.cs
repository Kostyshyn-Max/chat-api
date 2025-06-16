using ChatApp.Shared.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BusinessLogic.Interfaces;

public interface IMessageService
{
    Task<Guid> SaveMessageAsync(MessageModel message);
    Task<IEnumerable<MessageModel>> GetAllChatMessagesAsync(Guid chatId);
    Task<MessageModel?> GetMessageByIdAsync(Guid messageId);
}
