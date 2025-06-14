using AutoMapper;
using ChatApp.BusinessLogic.Interfaces;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Interfaces;
using ChatApp.Shared.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BusinessLogic.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public ChatService(IChatRepository chatRepository, IUserService userService, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Guid?> CreateChatAsync(Guid userId, ChatCreateModel model)
    {
        var secondUser = await _userService.GetUserByUsernameAsync(model.Username);
        if (secondUser is null || secondUser.Id.Equals(userId))
        {
            return null;
        }

        Chat chat = new()
        {
            User1Id = userId,
            User2Id = secondUser.Id,
        };

        var chatId = await _chatRepository.CreateAsync(chat);
        return chatId;
    }

    public async Task<IEnumerable<ChatModel>> GetAllUserChatsAsync(Guid userId)
    {
        var chatEntities = await _chatRepository.GetAllAsync(chat => chat.User1Id.Equals(userId) || chat.User2Id.Equals(userId));
        var chats = chatEntities.Select(_mapper.Map<ChatModel>).ToList();
        foreach (var chat in chats)
        {
            chat.OtherUser = await _userService.GetUserByIdAsync(chat.User1Id.Equals(userId) ? chat.User2Id : chat.User1Id);
        }

        return chats;
    }
}
