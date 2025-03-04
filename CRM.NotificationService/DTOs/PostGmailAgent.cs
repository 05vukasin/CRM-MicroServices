namespace CRM.NotificationService.DTOs
{
    public class PostGmailAgent
    {
        public string Email { get; set; } = string.Empty; // Gmail adresa
        public string AppPassword { get; set; } = string.Empty; // Google App Password ili OAuth token
    }
}
