using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rosmery.Security.API.Commands;
using Rosmery.Security.API.Models;
using Rosmery.Security.Identity.Managers;
using Rosmery.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosmery.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SECURITY_ADMINISTRATOR")]
    public class RoleController : ControllerBase
    {
        private readonly SecurityRoleManager<Role> _roleManager;
        private readonly SecurityUserManager<User> _userManager;
        private const string SECURITY_ADMINISTRATOR = "SECURITY_ADMINISTRATOR";

        public RoleController(
            SecurityRoleManager<Role> roleManager,
            SecurityUserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

            
            
        }

        [HttpGet("GetRoles")]
        public IEnumerable<RoleModel> GetAllRoles() {
            return _roleManager.GetAllRoles()
                .Where( r => r.Name != SECURITY_ADMINISTRATOR )
                .Select(r => new RoleModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    HasUsers = _userManager.GetUsersInRoleAsync(r.Name).Result?.Any()
                });
        }

        [HttpPost("CreateRole")]
        public async Task CreateRole(CreateRoleCommand command)
        {
            if (await _roleManager.RoleExistsAsync(command.Name))
                throw new Exception(string.Format("RoleName {0} already exist.", command.Name));

            await _roleManager.CreateAsync(new Role {
                Name = command.Name,
                Description = command.Description
            });
        }


        [HttpPut("UpdateRole")]
        public async Task UpdateRole(UpdateRoleCommand command)
        {
            var role = await _roleManager.FindByIdAsync(command.Id);

            if (role.Description == command.Description) return;

            role.Description = command.Description;

            await _roleManager.UpdateAsync(role);
        }

        [HttpDelete("DeleteRole/{roleId}")]
        public async Task DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            if (users.Any()) return;

            await _roleManager.DeleteAsync(role);
        }

        [HttpGet("ValidateRoleName/{roleName}")]
        public async Task<bool> ValidateRoleName(string roleName) {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}