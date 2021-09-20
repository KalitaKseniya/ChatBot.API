using ChatBot.API.Extensions;
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
using ChatBot.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        [HttpGet("testSer")]
        public IActionResult Test()
        {
            var perm = SerializeStatic.SerializeSuperClass(typeof(Permissions));
            //var json = JsonConvert.SerializeObject(perm);
            
            return Ok(perm);
        }

        [HttpGet]
        [Authorize(Policy = PolicyTypes.Claims.View)]
        public IActionResult GetAllPermissions()
        {
            var chatsPermissions = new { Permissions.Chats.AddRemove, Permissions.Chats.Edit, Permissions.Chats.ViewById };
            
            return Ok(chatsPermissions);
        }
    }
}
