using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Rosmery.Security.API.Controllers
{
    [Route("[controller]")]
    public class IdentityController : Controller
    {

        [HttpGet]
        [Authorize(Roles = "Security")]
        public IActionResult Get() {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

    }
}
