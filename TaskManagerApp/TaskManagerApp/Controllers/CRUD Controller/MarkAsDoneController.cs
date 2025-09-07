using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagerApp.Data;

namespace TaskManagerApp.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Route("Home")]
    public class MarkAsDoneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarkAsDoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: /Home/MarkAsDone
        [HttpPost("MarkAsDone")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await (from t in _context.Tasks
                              where t.Id == id && t.UserId == userId
                              select t).FirstOrDefaultAsync();

            if (task == null)
            {
                TempData["ErrorMessage"] = "Task not found.";
                return RedirectToAction("Index", "Index");
            }

            task.IsDone = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Task marked as done.";
            return RedirectToAction("Index", "Index");
        }
    }
}
