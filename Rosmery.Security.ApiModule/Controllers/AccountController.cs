using IdentityModel.Client;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Rosmery.Security.ApiModule.Models;
using Rosmery.Security.Core.IdentityManagers;
using Rosmery.Security.Core.IdentityModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rosmery.Security.ApiModule.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly SecurityUserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly string CustomerRoleName = "Customer";
        private readonly ILogger _logger;

        public AccountController(
            SecurityUserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            ILoggerFactory loggerFactory)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
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
            var roleExists = await _roleManager.RoleExistsAsync(CustomerRoleName);

            if (!roleExists)
                await _roleManager.CreateAsync(new Role() { Name = CustomerRoleName });

            await _userManager.AddToRoleAsync(user, CustomerRoleName);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            var tokenClient = new TokenClient(disco.TokenEndpoint, "rosmery-security", "rosmery-security-secret");
         
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(model.UserName, model.Password, "rosmery-security");

            if (tokenResponse.IsError)
                return Json(tokenResponse.ErrorDescription);

            return Json(tokenResponse.Json);
        }

    }
}
