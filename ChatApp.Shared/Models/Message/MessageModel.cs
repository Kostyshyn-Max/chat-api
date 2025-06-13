using ChatApp.Shared.Models.Chat;
using ChatApp.Shared.Models.User;

namespace ChatApp.Shared.Models.Message;

public class MessageModel
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    
    public UserModel Sender { get; set; }
    public ChatModel Chat { get; set; }
}