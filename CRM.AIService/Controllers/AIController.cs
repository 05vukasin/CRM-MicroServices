using CRM.AIService.Services;
using CRM.AIService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM.AIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly CRM.AIService.Services.AIService _aiService;

        public AIController(CRM.AIService.Services.AIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskAI([FromBody] AskAIRequest request)
        {
            var response = await _aiService.AskAIAsync(request.AIAgentId, request.Prompt);
            return Ok(new { Response = response }); // ✅ Vraća samo parsirani odgovor
        }
    }
}
