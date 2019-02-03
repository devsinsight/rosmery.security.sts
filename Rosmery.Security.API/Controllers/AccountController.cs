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

        public AccountController(
            SecurityUserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

    }
}
