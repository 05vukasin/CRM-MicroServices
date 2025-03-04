using CRM.NotificationService.Data;
using CRM.NotificationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.NotificationService.Controllers
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

        // ✅ Vraća sve GmailAgentUsage podatke zajedno sa GmailAgent podacima
        [HttpGet]
        public async Task<IActionResult> GetGmailAgentUsages()
        {
            var usages = await _context.GmailAgentUsages
                .Include(u => u.GmailAgent) // Uključujemo sve podatke o GmailAgent-u
                .ToListAsync();

            return Ok(usages);
        }

        // ✅ Vraća jedan zapis GmailAgentUsage sa svim podacima o agentu
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGmailAgentUsage(int id)
        {
            var usage = await _context.GmailAgentUsages
                .Include(u => u.GmailAgent) // Uključujemo podatke o GmailAgent-u
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usage == null)
            {
                return NotFound(new { Message = "GmailAgentUsage nije pronađen." });
            }

            return Ok(usage);
        }
    }
}
