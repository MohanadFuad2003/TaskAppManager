using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Route("Home")]
    public class EditController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EditController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Edit/{id}
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await (from t in _context.Tasks
                              where t.Id == id && t.UserId == userId
                              select t).FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            return View("~/Views/Home/Edit.cshtml", task);
        }

        // POST: /Home/Edit
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItem updatedTask)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Edit.cshtml", updatedTask);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await (from t in _context.Tasks
                              where t.Id == updatedTask.Id && t.UserId == userId
                              select t).FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            var oldDifficulty = task.Difficulty;
            var newDifficulty = updatedTask.Difficulty;

            task.Title = updatedTask.Title;
            task.IsDone = updatedTask.IsDone;
            task.Difficulty = newDifficulty;

            if (task.DueDate.HasValue)
            {
                if (oldDifficulty == "Easy" && newDifficulty == "Hard")
                    task.DueDate = task.DueDate.Value.AddDays(4);
                else if (oldDifficulty == "Medium" && newDifficulty == "Hard")
                    task.DueDate = task.DueDate.Value.AddDays(3);
                else if (oldDifficulty == "Easy" && newDifficulty == "Medium")
                    task.DueDate = task.DueDate.Value.AddDays(2);
                else if (oldDifficulty == "Hard" && newDifficulty == "Easy")
                    task.DueDate = task.DueDate.Value.AddDays(-4);
                else if (oldDifficulty == "Hard" && newDifficulty == "Medium")
                    task.DueDate = task.DueDate.Value.AddDays(-3);
                else if (oldDifficulty == "Medium" && newDifficulty == "Easy")
                    task.DueDate = task.DueDate.Value.AddDays(-2);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Task updated successfully.";
            return RedirectToAction("Index", "Index");
        }
    }
}
