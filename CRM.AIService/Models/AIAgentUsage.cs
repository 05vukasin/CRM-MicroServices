namespace CRM.AIService.Models
{
    public class AIAgentUsage
    {
        public int Id { get; set; } // Primarni ključ
        public int AIAgentId { get; set; } // Povezani AI agent
        public string Prompt { get; set; } = string.Empty; // Input (zahtev) koji je poslat AI agentu
        public string Response { get; set; } = string.Empty; // Odgovor AI agenta
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow; // Vreme kada je zahtev poslat
        public bool IsSuccess { get; set; } // Da li je odgovor bio uspešan
        public string? ErrorMessage { get; set; } // Poruka o grešci ako postoji

        // Navigaciono svojstvo za povezivanje sa AIAgent modelom
        public AIAgent? AIAgent { get; set; }
    }
}
