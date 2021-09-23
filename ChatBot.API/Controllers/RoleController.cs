using ChatBot.Core.Dtos;
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
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
        private readonly IRepositoryManager _repository;
        public RoleController(RoleManager<IdentityRole> roleManager,
                              ILoggerManager logger,
                              IRepositoryManager repositoryManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _repository = repositoryManager;
        }

        [HttpGet]
        [Authorize(Policy = PolicyTypes.Roles.View)]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }


        [HttpGet("{roleId}")]
        [Authorize(Policy = PolicyTypes.Roles.View)]
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
            //проверка полученных прав доступа на наличие в БД
            var permissions = _repository.Permission.Get(false).Select(p => p.Name);
            foreach (var perm in roleDto.Permissions)
            {
                if (!permissions.Contains(perm.Name))
                {
                    _logger.LogError($"No permission {perm} in database");
                    return NotFound();
                }
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleDto.Name));
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            var role = await _roleManager.FindByNameAsync(roleDto.Name);
            foreach (var claim in roleDto.Permissions)
            {
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, claim.Name));
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
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet("{roleName}/permissions")]
        [Authorize(Policy = PolicyTypes.Roles.ViewClaims)]
        public async Task<IActionResult> GetRolePermissions(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }
            var rolePermissions = (await _roleManager.GetClaimsAsync(role).ConfigureAwait(false))
                                    .Where(c => c.Type == CustomClaimTypes.Permission)
                                    .Select(p => p.Value);
            var permissions = _repository.Permission.Get(false).Where(p => rolePermissions.Contains(p.Name));
            return Ok(permissions);
        }

        //ToDo: переписать запрос с исп linq(статья на метаните) + check
        [HttpPut("{roleName}/permissions")]
        [Authorize(Policy = PolicyTypes.Roles.EditClaims)]
        public async Task<IActionResult> SetRolePermissions(string roleName, [FromBody] List<Permission> newPermissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }

            //проверка полученных прав доступа на наличие в БД
            //var permissions = _repository.Permission.Get(false);
            //if(newPermissions.Any(np => !permissions.Contains(np)))
            //{
            //    return NotFound();
            //}

            var permissionsToAdd = newPermissions;
            var claimsToDelete = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claimsToDelete)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            foreach (var claim in permissionsToAdd)
            {
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, claim.Name));
            }

            return Ok();
        }

        [HttpPost("{roleName}/permissions")]
        [Authorize(Policy = PolicyTypes.Roles.EditClaims)]
        public async Task<IActionResult> AddRolePermissions(string roleName, [FromBody] List<PermissionForRoleDto> newPermissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }

            //проверка полученных прав доступа на наличие в БД
            var permissions = _repository.Permission.Get(false).Select(p => p.Name);
            foreach (var newPermission in newPermissions)
            {
                if (!permissions.Contains(newPermission.Name))
                {
                    _logger.LogError($"No permission {newPermission} in database");
                    return NotFound();
                }
            }

            var rolePermissions = (await _roleManager.GetClaimsAsync(role)).Where(c => c.Type == CustomClaimTypes.Permission);
            var permissionsToAdd = newPermissions.Where(np => !rolePermissions.Contains(new Claim(CustomClaimTypes.Permission, np.Name)));//еще нет в rolePermisions

            foreach (var claim in permissionsToAdd)
            {
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, claim.Name));
            }

            foreach (var p in permissionsToAdd)
            {
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, p.Name));
            }

            return Ok();
        }
    }
}
