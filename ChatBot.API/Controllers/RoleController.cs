using ChatBot.Core.Dtos;
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
using ChatBot.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/admin/roles")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager,
                              ILoggerManager logger,
                              UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        //[HttpPost("init")]
        //public async Task<IActionResult> InitData()
        //{
        //    //await _roleManager.CreateAsync(new IdentityRole("Admin"));

        //    await _roleManager.CreateAsync(new IdentityRole("User"));

        //    var userRole = await _roleManager.FindByNameAsync("User");

        //    //await _roleManager.AddClaimAsync(userRole, new Claim(CustomClaimTypes.Permission, Permissions.Chats.Delete));
           
        //    //await _roleManager.AddClaimAsync(userRole, new Claim(CustomClaimTypes.Permission, Permissions.Users.Add));

        //    //var adminRole = await _roleManager.FindByNameAsync("Admin");

        //    //await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.Users.Add));

        //    //await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.Chats.));
        //    return Ok();

        //}

        [HttpGet]
        [Authorize(Policy = PolicyTypes.Roles.View)]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }


        [HttpGet("{roleId}")]
        [Authorize(Policy = PolicyTypes.Roles.View)]//??
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                _logger.LogError($"There is no role with id = {roleId}");
                return NotFound();
            }

            return Ok(role);
        }

        [HttpGet]
        [Route("name")]
        [Authorize(Policy = PolicyTypes.Roles.View)]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
            {
                _logger.LogError($"There is no role with name = {name}");
                return NotFound();
            }

            return Ok(role);
        }


        [HttpPost]
        [Authorize(Policy = PolicyTypes.Roles.AddRemove)]
        public async Task<IActionResult> CreateRole(RoleForCreationDto roleDto)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleDto.Name));
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpDelete("{roleId}")]
        [Authorize(Policy = PolicyTypes.Roles.AddRemove)]
        public async Task<IActionResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet("{roleName}")]
        [Authorize(Policy = PolicyTypes.Roles.ViewClaims)]
        public async Task<IActionResult> GetRoleClaims(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }
            var claims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);

            return Ok(claims);
        }

        [HttpPut("{roleName}")]
        [Authorize(Policy = PolicyTypes.Roles.EditClaims)]
        public async Task<IActionResult> SetRoleClaims(string roleName, [FromBody] List<string> claims)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            var claimsToDelete = roleClaims.Where(r => !claims.Contains(r.Value));
            var claimsToAdd = claims.Except(roleClaims.Select(r => r.Value));

            foreach (var claim in claimsToAdd)
            {
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, claim));
            }
            foreach (var claim in claimsToDelete)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            return Ok();
        }
    }
}
