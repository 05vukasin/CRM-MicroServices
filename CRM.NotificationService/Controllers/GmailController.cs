using CRM.NotificationService.Services;
using CRM.NotificationService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public GmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSent = await _emailService.SendEmailWithLoggingAsync(
                request.GmailAgentId,
                request.ToEmail,
                request.Subject,
                request.Body
            );

            if (isSent)
            {
                return Ok(new { Message = "Email uspešno poslat." });
            }
            else
            {
                return StatusCode(500, new { Message = "Došlo je do greške prilikom slanja emaila." });
            }
        }
    }
}
