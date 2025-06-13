using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Entities;

[Table("chats")]
[Index(nameof(User1Id), nameof(User2Id), IsUnique = true)]
public class Chat : BaseEntity
{
    [Column("user_1_id")]
    public Guid User1Id { get; set; }
    
    [Column("user_2_id")]
    public Guid User2Id { get; set; }
    
    public ICollection<Message>? Messages { get; set; }
}