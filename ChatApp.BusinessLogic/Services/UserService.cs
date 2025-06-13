using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using AutoMapper;
using ChatApp.BusinessLogic.Interfaces;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Interfaces;
using ChatApp.Shared.Models.User;

namespace ChatApp.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IJwtService jwtService, IUserRepository userRepository, IMapper mapper)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<Guid> RegisterUserAsync(UserRegisterModel model)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(32);
        byte[] password = Encoding.UTF8.GetBytes(model.Password);
        
        string hashedPassword = GenerateHash(password, salt);
        string saltString = Convert.ToBase64String(salt);

        User user = new()
        {
            Username = model.Username,
            PhoneNumber = model.PhoneNumber,
            PasswordHash = hashedPassword,
            PasswordSalt = saltString,
            RegisteredAt = DateTime.UtcNow,
        };
        
        var userId = await _userRepository.CreateAsync(user);
        return userId;
    }

    public Task<UserTokenDataModel?> LoginUserAsync(UserLoginModel model)
    {
        throw new NotImplementedException();
    }

    public Task<UserTokenDataModel?> RefreshTokenAsync(UserTokenDataModel tokenModel)
    {
        throw new NotImplementedException();
    }
    
    private string GenerateHash(byte[] password, byte[] salt)
    {
        byte[] hash = SHA256.HashData(password.Concat(salt).ToArray());
        return Convert.ToBase64String(hash);
    }
}