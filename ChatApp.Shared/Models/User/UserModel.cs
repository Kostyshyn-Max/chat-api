using ChatApp.Shared.Models.Chat;

namespace ChatApp.Shared.Models.User;

public class UserModel
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    
    public ICollection<ChatModel>? Chats { get; set; }
}