using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using AutoMapper;
using ChatApp.BusinessLogic.Interfaces;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Interfaces;
using ChatApp.Shared.Models.Chat;
using ChatApp.Shared.Models.User;
using Microsoft.Extensions.Configuration;

namespace ChatApp.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(IJwtService jwtService, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
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

    public async Task<UserTokenDataModel?> LoginUserAsync(UserLoginModel model)
    {
        var passwordSalt = await _userRepository.GetUserPasswordSaltAsync(model.PhoneNumber);
        if (passwordSalt is null)
        {
            return null;
        }

        byte[] password = Encoding.UTF8.GetBytes(model.Password);
        string hashedPassword = GenerateHash(password, Convert.FromBase64String(passwordSalt));

        var user = await _userRepository.LoginAsync(model.PhoneNumber, hashedPassword);
        if (user is null)
        {
            return null;
        }

        string token = _jwtService.GenerateAccessToken(_mapper.Map<UserModel>(user));
        string? refreshToken = await SetNewRefreshToken(user.Id);
        if (refreshToken is null)
        {
            return null;
        }

        var tokenData = new UserTokenDataModel() { Token = token, RefreshToken = refreshToken };
        return tokenData;
    }

    public async Task<UserTokenDataModel?> RefreshTokenAsync(UserTokenDataModel oldTokenModel)
    {
        ClaimsPrincipal? principals = _jwtService.GetPrincipalFromExpiredToken(oldTokenModel.Token);
        if (principals is null)
        {
            return null;
        }

        var id = principals.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id is null)
        {
            return null;
        }

        Guid userId = Guid.Parse(id);
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return null;
        }

        if (user.RefreshToken is null || !user.RefreshToken.Equals(oldTokenModel.RefreshToken))
        {
            return null;
        }

        string? newRefreshToken = await SetNewRefreshToken(userId);
        if (newRefreshToken is null)
        {
            return null;
        }

        string newToken = _jwtService.GenerateAccessToken(_mapper.Map<UserModel>(user));
        var tokenData = new UserTokenDataModel() { Token = newToken, RefreshToken = newRefreshToken };
        return tokenData;
    }
    
    private static string GenerateHash(byte[] password, byte[] salt)
    {
        byte[] hash = SHA256.HashData(password.Concat(salt).ToArray());
        return Convert.ToBase64String(hash);
    }

    private async Task<string?> SetNewRefreshToken(Guid userId)
    {
        string refreshToken = _jwtService.GenerateRefreshToken();
        DateTime expireDate = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:RefreshTokenExpirationDays"] ?? "7"));
        var result = await _userRepository.SetRefreshToken(userId, refreshToken, expireDate);
        return result ? refreshToken : null;
    }

    public async Task<UserModel?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return null;
        }

        var mappedUser = _mapper.Map<UserModel>(user);
        return mappedUser;
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        var user = (await _userRepository.GetAllAsync(u => u.Username.Equals(username))).FirstOrDefault();
        return user is null ? null : _mapper.Map<UserModel>(user);
    }
}