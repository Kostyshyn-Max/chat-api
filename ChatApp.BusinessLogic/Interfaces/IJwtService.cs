using System.Security.Claims;
using ChatApp.Shared.Models.User;

namespace ChatApp.BusinessLogic.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(UserModel userModel);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}