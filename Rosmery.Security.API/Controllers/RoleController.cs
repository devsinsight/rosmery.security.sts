using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rosmery.Security.Identity.Managers;
using Rosmery.Security.Identity.Models;

namespace Rosmery.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly SecurityRoleManager<Role> _roleManager;

        public RoleController(
            SecurityRoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("GetRoles")]
        public IEnumerable<Role> GetAllRoles() {
            return _roleManager.GetAllRoles();
        }
    }
}