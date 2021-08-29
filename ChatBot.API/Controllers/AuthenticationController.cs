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
        public AuthenticationController(UserManager<User> userManager,
                                        RoleManager<User> roleManager, 
                                        ILoggerManager logger)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
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
    }
}
