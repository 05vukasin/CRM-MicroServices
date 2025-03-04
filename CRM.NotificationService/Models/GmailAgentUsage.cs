namespace CRM.NotificationService.Models
{
    public class GmailAgentUsage
    {
        public int Id { get; set; } // Primarni ključ
        public int GmailAgentId { get; set; } // Povezani GmailAgent
        public string RecipientEmail { get; set; } = string.Empty; // Primaoc emaila
        public string Subject { get; set; } = string.Empty; // Naslov poruke
        public DateTime SentAt { get; set; } = DateTime.UtcNow; // Vreme slanja
        public bool IsSuccess { get; set; } // Da li je email uspešno poslat
        public string? ErrorMessage { get; set; } // Poruka o grešci ako postoji

        // Navigaciona svojstva
        public GmailAgent? GmailAgent { get; set; }
    }
}
