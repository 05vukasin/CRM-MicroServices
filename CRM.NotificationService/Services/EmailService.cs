using System.Net;
using System.Net.Mail;
using CRM.NotificationService.Data;
using CRM.NotificationService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.NotificationService.Services
{
    public class EmailService
    {
        private readonly DataContext _context;

        public EmailService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SendEmailWithLoggingAsync(int gmailAgentId, string toEmail, string subject, string body)
        {
            // Pronalazi Gmail agenta
            var gmailAgent = await _context.GmailAgents.FindAsync(gmailAgentId);
            if (gmailAgent == null || !gmailAgent.IsActive)
            {
                throw new InvalidOperationException("GmailAgent ne postoji ili nije aktivan.");
            }

            var usageLog = new GmailAgentUsage
            {
                GmailAgentId = gmailAgent.Id,
                RecipientEmail = toEmail,
                Subject = subject,
                SentAt = DateTime.UtcNow
            };

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(gmailAgent.Email, gmailAgent.AppPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(gmailAgent.Email),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);

                // Uspešno poslato, beležimo ulog
                usageLog.IsSuccess = true;
                _context.GmailAgentUsages.Add(usageLog);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                usageLog.IsSuccess = false;
                usageLog.ErrorMessage = ex.Message;
                _context.GmailAgentUsages.Add(usageLog);
                await _context.SaveChangesAsync();

                return false;
            }
        }
    }
}
