using ChatApp.Shared.Models.User;

namespace ChatApp.BusinessLogic.Interfaces;

public interface IUserService
{
    Task<Guid> RegisterUserAsync(UserRegisterModel model);
    Task<UserTokenDataModel?> LoginUserAsync(UserLoginModel model);
    Task<UserTokenDataModel?> RefreshTokenAsync(UserTokenDataModel tokenModel);
}