
using Microsoft.AspNetCore.Mvc;
using TODO.Dtos;
using TODO.Models;
using TODO.Repository;
using static TODO.Dtos.UserDto;

namespace TODO.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    

    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userController;

    public UserController(ILogger<UserController> logger, IUserRepository userController)
    {
        _logger = logger;
        _userController = userController;
    }

    [HttpGet]

    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var userList = await _userController.GetList();

         var dtoList = userList;  //Select(x => x.asDto);

        return Ok(userList);
    }
    

    [HttpGet("{user_id}")]
    public async Task<ActionResult<UserDto>> GetUserById([FromRoute]long user_id)
    {
        var user = await _userController.GetById(user_id);
        if (user is null)
        {
            return NotFound("No User found with this id");
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto Data)
    {
        var toCreateUser = new User
        {
            Name = Data.Name,
            Password = Data.Password,
            Email = Data.Email,
            Mobile = Data.Mobile,
            Address = Data.Address,
        };
        var res = await _userController.Create(toCreateUser);
        return StatusCode(201, res);
    }
    [HttpPut("{user_id}")]
    public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] long user_id, [FromBody] UpdateUserDto Data)
    {
       var existing = await _userController.GetById(user_id);
        if (existing is null)
        
            return NotFound("No User found with this id");
        var toUpdateUser = existing with
        {
            Name = Data.Name?.Trim() ?? existing.Name,
            Password = Data.Password?.Trim() ?? existing.Password,
            Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            Mobile = Data.Mobile,
            Address = Data.Address ?.Trim()?.ToLower() ?? existing.Address,
        };
        var didUpdate = await _userController.Update(toUpdateUser);
        if (!didUpdate)
        
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update User");
        return NoContent();
        
            
         
    }

    [HttpDelete("{user_id}")]

    public async Task<ActionResult<UserDto>> DeleteUser([FromRoute] long user_id)
    {
        var existing = await _userController.GetById(user_id);
        if (existing is null)
        
            return NotFound("No User found with this id");
        
        var didDelete = await _userController.Delete(user_id);
        return NoContent();
    }

}
