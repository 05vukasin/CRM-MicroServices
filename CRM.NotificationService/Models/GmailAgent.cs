namespace CRM.NotificationService.Models
{
    public class GmailAgent
    {
        public int Id { get; set; } // Primarni ključ u bazi
        public string Email { get; set; } = string.Empty; // Gmail adresa
        public string AppPassword { get; set; } = string.Empty; // Google App Password ili OAuth token
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Datum kreiranja
        public bool IsActive { get; set; } = true; // Da li je aktivan
    }
}
