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
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController( 
                                        ILoggerManager logger,
                                        IAuthenticationManager authenticationManager)
        {
            _logger = logger;
            _authManager = authenticationManager;
        }

        /// <summary>
        /// Login the system
        /// </summary>
        /// <returns>The token</returns>
        [HttpPost]
        [Route("login")]
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
