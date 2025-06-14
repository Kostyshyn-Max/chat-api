using ChatApp.Shared.Models.User;

namespace ChatApp.BusinessLogic.Interfaces;

public interface IUserService
{
    Task<Guid> RegisterUserAsync(UserRegisterModel model);
    Task<UserTokenDataModel?> LoginUserAsync(UserLoginModel model);
    Task<UserTokenDataModel?> RefreshTokenAsync(UserTokenDataModel oldTokenModel);
    Task<UserModel?> GetUserByIdAsync(Guid id);
    Task<UserModel?> GetUserByUsernameAsync(string username);   
}