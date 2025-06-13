using System.ComponentModel.DataAnnotations;

namespace ChatApp.Shared.Models.User;

public class UserRegisterModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Phone]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{8,32}")]
    public string Password { get; set; } = string.Empty;
}