using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    [Authorize]
    [Route("Account")]
    public class EditProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EditProfileController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/EditProfile
        [HttpGet("EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Login");

            var fullName = (User.Identity as ClaimsIdentity)?
                           .FindFirst("FullName")?.Value ?? "";

            var vm = new EditProfileViewModel
            {
                FullName = fullName,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? ""
            };
            return View("~/Views/Account/EditProfile.cshtml", vm);
        }

        // POST: /Account/EditProfile
        [HttpPost("EditProfile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/EditProfile.cshtml", model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Login");

            // تحديث البريد واسم المستخدم (نجعل UserName = Email) + الهاتف
            user.Email = model.Email?.Trim();
            user.UserName = model.Email?.Trim();
            user.PhoneNumber = model.PhoneNumber?.Trim();

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                foreach (var e in updateUserResult.Errors)
                    ModelState.AddModelError(string.Empty, e.Description);

                return View("~/Views/Account/EditProfile.cshtml", model);
            }

            // تحديث Claim الاسم الكامل
            var claims = await _userManager.GetClaimsAsync(user);
            var oldFullNameClaim = claims.FirstOrDefault(c => c.Type == "FullName");
            if (oldFullNameClaim != null)
                await _userManager.RemoveClaimAsync(user, oldFullNameClaim);

            if (!string.IsNullOrWhiteSpace(model.FullName))
                await _userManager.AddClaimAsync(user, new Claim("FullName", model.FullName.Trim()));

            // تحديث الكوكيز (لنعكس الاسم فوراً في الهيدر)
            await _signInManager.RefreshSignInAsync(user);

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToAction("EditProfile", "EditProfile");
        }

        // GET: /Account/ChangePassword
        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View("~/Views/Account/ChangePassword.cshtml", new ChangePasswordViewModel());
        }

        // POST: /Account/ChangePassword
        [HttpPost("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/ChangePassword.cshtml", model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Login");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessage"] = "Password changed successfully.";
                return RedirectToAction("ChangePassword", "EditProfile");
            }

            foreach (var e in result.Errors)
                ModelState.AddModelError(string.Empty, e.Description);

            return View("~/Views/Account/ChangePassword.cshtml", model);
        }
    }
}
