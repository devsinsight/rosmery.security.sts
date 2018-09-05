using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rosmery.Security.API.IdentityServer;
using Rosmery.Security.API.Models;
using Rosmery.Security.Identity.Managers;
using Rosmery.Security.Identity.Models;
using System.Threading.Tasks;

namespace Rosmery.Security.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly SecurityUserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IClientCredentials _clientCredentials;
        private readonly string SecurityRoleName = "Security";

        public AccountController(
            SecurityUserManager<User> userManager,
            RoleManager<Role> roleManager,
            IClientCredentials clientCredentials)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _clientCredentials = clientCredentials;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await AddRoleAsync(user);
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        public async Task AddRoleAsync(User user)
        {
            var roleExists = await _roleManager.RoleExistsAsync(SecurityRoleName);

            if (!roleExists)
                await _roleManager.CreateAsync(new Role() { Name = SecurityRoleName });

            await _userManager.AddToRoleAsync(user, SecurityRoleName);
        }

    }
}
