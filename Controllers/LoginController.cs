using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODO.Models;
using TODO.Repository;

namespace TODO.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public UserLoginController(IConfiguration configuration,IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
 
    

        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] UserLogin userlogin)
        {
            var currentUser = await _userRepository.GetById(userlogin.UserId);
        if (currentUser == null)
          return NotFound("user not found");

        if(currentUser.Password != userlogin.Password)
            return Unauthorized("Invalid password");
        var token = Generate(currentUser);
        return Ok(token);
        //    var user = await Authenticate(userlogin);

        //    if (user != null)
        //    {
        //        var token = Generate(user);
        //        return Ok(token);
        //    }
        //     return NotFound("User not found");
        }
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration ["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.SerialNumber, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                // new Claim(ClaimTypes.NameIdentifier, user.Password),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Mobile.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Address)

                
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(25),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<User> Authenticate(UserLogin userLogin)
        {
            var currentUser = await _userRepository.GetById(userLogin.UserId);
            
            Console.WriteLine(currentUser.UserId);

            if (currentUser == null)
            {
                return null;
            }
            Console.WriteLine("db password" + currentUser.Password);
            Console.WriteLine("user password" + userLogin.Password);

            if (currentUser.Password != userLogin.Password)
            {
                return null;
            }
            return currentUser;
        }
    }
}   