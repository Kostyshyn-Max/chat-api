using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.DataAccess.Entities;

[Table("users")]
public class User : BaseEntity
{
    [Column("username")]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Column("phone_number")]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Column("registered_at")]
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("password_salt")]
    public string PasswordSalt { get; set; } = string.Empty;

    [Column("refresh_token")] 
    public string? RefreshToken { get; set; } = null;

    [Column("refresh_token_expire_date")]
    public DateTime? RefreshTokenExpireDate { get; set; }
    
    public ICollection<Chat>? Chats { get; set; }
}