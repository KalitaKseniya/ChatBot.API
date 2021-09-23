using ChatBot.API.Extensions;
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
using ChatBot.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


//ToDo: auto in startup
//toDo: UI representation?? simple page 401 Forbidden,
//                                  users pages
//                                  user page (with roles - roles are active links and checkboxes)
//                                  roles page
//                                  role page (with permissions - show all permissions, they are checkboxes)

//todo: ЛК
//todo: Chats
//todo: Edit user roles
//todo: рефакторинг
//todo: setrole norm

namespace ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/admin/permissions")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryManager _repositoryManager;
        public PermissionController(RoleManager<IdentityRole> roleManager,
                              ILoggerManager logger,
                              UserManager<User> userManager, 
                              IRepositoryManager repositoryManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
            _repositoryManager = repositoryManager;
        }

        //[HttpGet("testSer")]
        //public IActionResult Test()
        //{
        //    var perm = SerializeStatic.SerializeSuperClass(typeof(Permissions));
        //    //var json = JsonConvert.SerializeObject(perm);
            
        //    return Ok(perm);
        //}

        [HttpGet]
        [Authorize(Policy = PolicyTypes.Claims.View)]
        public IActionResult GetAllPermissions()
        {
            var permissions = _repositoryManager.Permission.Get(false);
            
            return Ok(permissions);
        }
    }
}
