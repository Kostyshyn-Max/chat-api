using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.DataAccess.Entities;

[Table("messages")]
public class Message : BaseEntity
{
    [Column("chat_id")]
    [ForeignKey(nameof(Chat))]
    public Guid ChatId { get; set; }
    
    [Column("sender_id")]
    [ForeignKey(nameof(Sender))]
    public Guid SenderId { get; set; }
    
    [Column("content")]
    [MaxLength(4000)]
    public string Content { get; set; } = string.Empty;
    
    [Column("sent_at")]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    [Column("is_read")]
    public bool IsRead { get; set; } = false;
    
    public Chat Chat { get; set; } = null!;
    public User Sender { get; set; } = null!;
}
