using ChatApp.Shared.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BusinessLogic.Interfaces;

public interface IChatService
{
    Task<IEnumerable<ChatModel>> GetAllUserChatsAsync(Guid userId);
    Task<Guid?> CreateChatAsync(Guid userId, ChatCreateModel model);
}
