using System.ComponentModel.DataAnnotations;

namespace ChatApp.Shared.Models.User;

public class UserLoginModel
{
    [Phone]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}