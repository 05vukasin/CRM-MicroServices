namespace CRM.AIService.DTOs
{
    public class AskAIRequest
    {
        public int AIAgentId { get; set; } // ID AI agenta
        public string Prompt { get; set; } = string.Empty; // Upit za AI
    }
}
