using ChatApp.Shared.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.Models.User;

public class UserDetailsModel : UserModel
{
    public ICollection<ChatModel>? Chats { get; set; }
}
