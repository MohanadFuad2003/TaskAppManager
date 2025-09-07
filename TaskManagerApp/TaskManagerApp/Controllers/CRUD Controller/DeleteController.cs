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
    public class DeleteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeleteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Delete/{id}
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await (from t in _context.Tasks
                              where t.Id == id && t.UserId == userId
                              select t).FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            if (task.IsDone == false)
            {
                TempData["ErrorMessage"] = "You can only delete completed tasks.";
                return RedirectToAction("Index", "Index");
            }

            return View("~/Views/Home/Delete.cshtml", task);
        }

        // POST: /Home/Delete
        [HttpPost("Delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await (from t in _context.Tasks
                              where t.Id == id && t.UserId == userId
                              select t).FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Task deleted successfully.";
            return RedirectToAction("Index", "Index");
        }
    }
}
