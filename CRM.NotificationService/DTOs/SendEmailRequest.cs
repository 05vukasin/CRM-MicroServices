namespace CRM.NotificationService.DTOs
{
    public class SendEmailRequest
    {
        public int GmailAgentId { get; set; } // ID agenta koji šalje email
        public string ToEmail { get; set; } = string.Empty; // Primaoc emaila
        public string Subject { get; set; } = string.Empty; // Naslov emaila
        public string Body { get; set; } = string.Empty; // Sadržaj emaila
    }
}
