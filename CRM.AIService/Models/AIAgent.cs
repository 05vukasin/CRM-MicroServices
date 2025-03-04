using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRM.AIService.Models
{
    public class AIAgent
    {
        public int Id { get; set; } // Primarni ključ
        public string Name { get; set; } = string.Empty; // Naziv agenta (npr. "Marketing AI")
        public AI AIModel { get; set; }

        public string RolePrompt { get; set; } = string.Empty; // Uloga agenta (npr. "Marketing")
        public string ApiKey { get; set; } = string.Empty; // API ključ za AI servis
        public bool IsActive { get; set; } = true; // Da li je agent aktivan
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Datum kreiranja
    }

    public enum AI
    {
        Gemini,    // Google AI
        ChatGPT,   // OpenAI
        DeepSeek   // DeepSeek AI
    }
}
