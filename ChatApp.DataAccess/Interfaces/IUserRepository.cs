using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Interfaces;

public interface IUserRepository : ICrudRepository<User>
{
    Task<string?> GetUserPasswordSaltAsync(string phoneNumber);
    Task<User?> LoginAsync(string phoneNumber, string passwordHash); 
    Task<bool> SetRefreshToken(Guid userId, string refreshToken, DateTime expireDate);
}