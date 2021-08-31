using ChatBot.Core.Dtos;
using ChatBot.Core.Interfaces;
using ChatBot.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController(UserManager<User> userManager,
                                        RoleManager<User> roleManager, 
                                        ILoggerManager logger,
                                        IAuthenticationManager authenticationManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _authManager = authenticationManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserForCreationDto userDto)
        {
            if(userDto == null)
            {
                _logger.LogWarn("UserDto can't be null");
                return BadRequest();
            }

            var user = new User() {
                UserName = userDto.UserName,
                Email = userDto.Email,
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, userDto.Roles);
            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(UserForAuthenticationDto userDto)
        {
            if (!await _authManager.ValidateUser(userDto))
            {
                _logger.LogWarn($"Authentication of user failed. {userDto.UserName} {userDto.Password}");
                return Unauthorized();
            }
            var token = await _authManager.CreateToken() ;
            return Ok(new { token = token });
        }
    }
}
