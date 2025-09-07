using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    [Route("Account")]
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: /Account/Login
        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (TempData["SuccessMessage"] != null)
                ViewBag.SuccessMessage = TempData["SuccessMessage"];

            ViewData["ReturnUrl"] = returnUrl;
            return View("~/Views/Account/Login.cshtml");
        }

        // POST: /Account/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/Login.cshtml", model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // أولاً: لو في returnUrl محلي، نرجع له
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                // بدلاً من Home (اللي انحذف)، نوجّه إلى IndexController → Index
                return RedirectToAction("Index", "Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View("~/Views/Account/Login.cshtml", model);
        }
    }
}
