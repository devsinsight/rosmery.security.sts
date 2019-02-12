using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Rosmery.Security.API.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "SECURITY_ADMINISTRATOR")]
    public class IdentityController : Controller
    {

        [HttpGet]
        public IActionResult Get() {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

    }
}
