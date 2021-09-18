using ChatBot.Core.Interfaces;
using ChatBot.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/admin/permissions")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        public PermissionController(RoleManager<IdentityRole> roleManager,
                              ILoggerManager logger,
                              UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult GetAllPermissions()
        {

        }

        [HttpGet("{userId}")]
        public IActionResult GetUserPermissions(string userId)
        {

        }

        [HttpPut("{userId}")]
        public IActionResult EditUserPermissions(string userId)
        {

        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUserPermission()
        {

        }

    }
}
