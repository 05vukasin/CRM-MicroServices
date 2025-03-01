using CRM.LeadService.Data;
using CRM.LeadService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.LeadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly DataContext _context;

        public LeadController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Lead>>> GetLeads()
        {
            return await _context.Leads.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Lead>> GetLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);

            if (lead == null)
            {
                return NotFound();
            }

            return lead;
        }

        [HttpPost]
        public async Task<ActionResult<PostLead>> ActionResult(PostLead postLead)
        {
            var lead = new Models.Lead
            {
                Name = postLead.Name,
                Email = postLead.Email,
                Phone = postLead.Phone,
                Address = postLead.Address
            };

            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();

            return Ok("Crated Sucesfully");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLead(int id, PostLead postLead)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            lead.Name = postLead.Name;
            lead.Email = postLead.Email;
            lead.Phone = postLead.Phone;
            lead.Address = postLead.Address;

            await _context.SaveChangesAsync();

            return Ok("Updated Sucessfuly");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();

            return Ok("Deleted Sucessfuly");
        }
    }
}
