using AutoMapper;
using ChatApp.DataAccess.Entities;
using ChatApp.Shared.Models.Chat;
using ChatApp.Shared.Models.Message;
using ChatApp.Shared.Models.User;

namespace ChatApp.Shared.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<User, UserModel>();
        this.CreateMap<User, UserDetailsModel>();
        this.CreateMap<Chat, ChatModel>();
        this.CreateMap<ChatModel, Chat>();
        this.CreateMap<Message, MessageModel>();
        this.CreateMap<MessageModel, Message>();
    }
}