using CRM.NotificationService.Data;
using CRM.NotificationService.DTOs;
using CRM.NotificationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GmailAgentController : ControllerBase
    {
        public readonly DataContext _context;

        public GmailAgentController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostGmailAgent(PostGmailAgent postGmailAgent)
        {
            var gmailAgent = new GmailAgent
            {
                Email = postGmailAgent.Email,
                AppPassword = postGmailAgent.AppPassword
            };

            _context.GmailAgents.Add(gmailAgent);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGmailAgent(int id)
        {
            var gmailAgent = await _context.GmailAgents.FindAsync(id);
            if (gmailAgent == null)
            {
                return NotFound();
            }

            _context.GmailAgents.Remove(gmailAgent);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetGmailAgents()
        {
            var gmailAgents = await _context.GmailAgents.ToListAsync();
            return Ok(gmailAgents);
        }
    }
}
