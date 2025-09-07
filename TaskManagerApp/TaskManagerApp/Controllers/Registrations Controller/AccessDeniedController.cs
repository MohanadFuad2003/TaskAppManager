using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApp.Controllers
{
    [Route("Account")]
    public class AccessDeniedController : Controller
    {
        // GET: /Account/AccessDenied
        [HttpGet("AccessDenied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Account/AccessDenied.cshtml");
        }
    }
}
