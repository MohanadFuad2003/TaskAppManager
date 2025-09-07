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
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Stats
        [HttpGet("Stats")]
        public async Task<IActionResult> Stats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            var stats = new TaskStatsViewModel
            {
                Total = tasks.Count,
                Done = tasks.Count(t => t.IsDone),
                NotDone = tasks.Count(t => !t.IsDone)
            };

            return View("~/Views/Home/Stats.cshtml", stats);
        }
    }
}
