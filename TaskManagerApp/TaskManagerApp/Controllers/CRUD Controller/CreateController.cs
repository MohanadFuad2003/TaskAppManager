using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Route("Home")]
    public class CreateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Home/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Home/Create.cshtml");
        }

        // POST: /Home/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Create.cshtml", task);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Login");
            }

            var difficulty = Request.Form["Difficulty"].ToString();
            int daysToAdd = 0;

            switch (difficulty)
            {
                case "Easy":
                    daysToAdd = 3 + 1;
                    break;
                case "Medium":
                    daysToAdd = 7 + 1;
                    break;
                case "Hard":
                    daysToAdd = 10 + 1;
                    break;
            }

            task.UserId = userId;
            task.CreatedAt = DateTime.Now;
            task.DueDate = task.CreatedAt.AddDays(daysToAdd);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Index");
        }
    }
}
