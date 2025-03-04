using CRM.AIService.Models;

namespace CRM.AIService.DTOs
{
    public class PostAIAgent
    {
        public string Name { get; set; } = string.Empty; // Naziv agenta (npr. "Marketing AI")
        public CRM.AIService.Models.AI AIModel { get; set; } // Tip AI modela (Gemini, ChatGPT, DeepSeek)
        public string RolePrompt { get; set; } = string.Empty; // Uloga agenta (npr. "Marketing")
        public string ApiKey { get; set; } = string.Empty; // API ključ za AI servis
    }
}
