using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Entities;

[Table("chats")]
[Index(nameof(User1Id), nameof(User2Id), IsUnique = true)]
public class Chat : BaseEntity
{
    [Column("user_1_id")]
    [ForeignKey(nameof(User1))]
    public Guid User1Id { get; set; }
    
    [Column("user_2_id")]
    [ForeignKey(nameof(User2))]
    public Guid User2Id { get; set; }
    
    public User User1 { get; set; } = null!;
    public User User2 { get; set; } = null!;
    
    public ICollection<Message>? Messages { get; set; }
}