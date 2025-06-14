using ChatApp.Shared.Models.Message;
using ChatApp.Shared.Models.User;

namespace ChatApp.Shared.Models.Chat;

public class ChatModel
{
    public Guid Id { get; set; }
    public Guid User1Id { get; set; }
    public Guid User2Id { get; set; }

    public UserModel? OtherUser { get; set; }
}