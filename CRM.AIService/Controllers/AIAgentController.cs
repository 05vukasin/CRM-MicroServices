using CRM.AIService.Data;
using CRM.AIService.DTOs;
using CRM.AIService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CRM.AIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIAgentController : ControllerBase
    {
        private readonly DataContext _context;

        public AIAgentController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostAIAgent(PostAIAgent postAIAgent)
        {
            var aiAgent = new AIAgent
            {
                Name = postAIAgent.Name,
                AIModel = postAIAgent.AIModel, // ✅ Direktno koristimo enum AI
                RolePrompt = postAIAgent.RolePrompt,
                ApiKey = postAIAgent.ApiKey,
                CreatedAt = DateTime.UtcNow
            };

            _context.AIAgents.Add(aiAgent);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAIAgent(int id)
        {
            var aiAgent = _context.AIAgents.Find(id);
            if (aiAgent == null)
            {
                return NotFound();
            }

            _context.AIAgents.Remove(aiAgent);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAIAgents()
        {
            var aiAgents = _context.AIAgents
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    AIModel = a.AIModel.ToString(), // ✅ Automatska konverzija iz enuma u string
                    a.RolePrompt,
                    a.ApiKey,
                    a.IsActive,
                    a.CreatedAt
                })
                .ToList();

            return Ok(aiAgents);
        }
    }
}
