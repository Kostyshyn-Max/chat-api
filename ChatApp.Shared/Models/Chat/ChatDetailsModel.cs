using ChatApp.Shared.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.Models.Chat;

public class ChatDetailsModel : ChatModel
{
    public ICollection<MessageModel>? Messages { get; set; }
}
