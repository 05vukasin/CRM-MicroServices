using CRM.AIService.Data;
using CRM.AIService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.AIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsageController : ControllerBase
    {
        private readonly DataContext _context;

        public UsageController(DataContext context)
        {
            _context = context;
        }

        // ✅ GET /api/usage - vraća sve Usage podatke
        [HttpGet]
        public async Task<IActionResult> GetAllUsage()
        {
            var usageLogs = await _context.AIAgentUsages
                .Include(u => u.AIAgent) // ✅ Uključujemo podatke o agentu
                .ToListAsync();

            return Ok(usageLogs);
        }

        // ✅ GET /api/usage/{aiAgentId} - vraća Usage podatke za određenog AI agenta
        [HttpGet("{aiAgentId}")]
        public async Task<IActionResult> GetUsageByAgent(int aiAgentId)
        {
            var usageLogs = await _context.AIAgentUsages
                .Where(u => u.AIAgentId == aiAgentId)
                .Include(u => u.AIAgent) // ✅ Uključujemo podatke o agentu
                .ToListAsync();

            if (usageLogs == null || usageLogs.Count == 0)
            {
                return NotFound($"Nema Usage podataka za AI agenta sa ID: {aiAgentId}");
            }

            return Ok(usageLogs);
        }
    }
}
