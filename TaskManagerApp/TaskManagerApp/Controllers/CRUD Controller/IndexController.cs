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
    public class IndexController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IndexController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Index
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string filter = "all")
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allUserTasksQuery =
                from t in _context.Tasks
                where t.UserId == userId
                select t;

            var total = await allUserTasksQuery.CountAsync();

            var doneCount = await
                (from t in _context.Tasks
                 where t.UserId == userId && t.IsDone == true
                 select t).CountAsync();

            var pendingCount = total - doneCount;

            ViewBag.TotalCount = total;
            ViewBag.DoneCount = doneCount;
            ViewBag.PendingCount = pendingCount;
            ViewBag.Filter = filter;

            List<TaskItem> tasks;

            if (filter == "done")
            {
                tasks = await
                    (from t in _context.Tasks
                     where t.UserId == userId && t.IsDone == true
                     select t).ToListAsync();
            }
            else if (filter == "pending")
            {
                tasks = await
                    (from t in _context.Tasks
                     where t.UserId == userId && t.IsDone == false
                     select t).ToListAsync();
            }
            else
            {
                tasks = await allUserTasksQuery.ToListAsync();
            }

            return View("~/Views/Home/Index.cshtml", tasks);
        }
    }
}
