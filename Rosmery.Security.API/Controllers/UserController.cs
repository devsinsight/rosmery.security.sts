using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rosmery.Security.API.Models;
using Rosmery.Security.Identity.Managers;
using Rosmery.Security.Identity.Models;

namespace Rosmery.Security.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly SecurityUserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserController(
            SecurityUserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("getUsers")]
        public List<UserModel> GetUsers()
        {
            return _userManager
                        .GetAllUsers()
                        .Select(u => new UserModel()
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            UserName = u.UserName,
                            PhoneNumber = u.PhoneNumber,
                            Roles = _userManager.GetRolesAsync(u).Result
                        }).ToList();

        }
    }
}