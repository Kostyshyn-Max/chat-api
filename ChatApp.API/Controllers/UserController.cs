using ChatApp.BusinessLogic.Interfaces;
using ChatApp.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]/")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [Route("register")]
    [ProducesResponseType<UserTokenDataModel>(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserTokenDataModel>> RegisterUserAsync([FromBody] UserRegisterModel model)
    {
        await _userService.RegisterUserAsync(model);
        var tokenData = await _userService.LoginUserAsync(new UserLoginModel
        {
            PhoneNumber = model.PhoneNumber,
            Password = model.Password
        });
        
        if (tokenData is null)
        {
            return BadRequest();
        }
        
        return Ok(tokenData);
    }
    
    [HttpPost]
    [Route("login")]
    [ProducesResponseType<UserTokenDataModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserTokenDataModel>> LoginUserAsync([FromBody] UserLoginModel model)
    {
        var result = await _userService.LoginUserAsync(model);
        if (result is null)
        {
            return Unauthorized();
        }
        
        return Ok(result);
    }
    
    [HttpPost]
    [Route("refresh")]
    [ProducesResponseType<UserTokenDataModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserTokenDataModel>> RefreshTokenAsync([FromBody] UserTokenDataModel tokenModel)
    {
        var result = await _userService.RefreshTokenAsync(tokenModel);
        if (result is null)
        {
            return Unauthorized();
        }
        
        return Ok(result);
    }
}