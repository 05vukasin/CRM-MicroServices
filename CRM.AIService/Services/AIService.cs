using System;
using System.Threading.Tasks;
using CRM.AIService.AI;
using CRM.AIService.Data;
using CRM.AIService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.AIService.Services
{
    public class AIService
    {
        private readonly DataContext _context;
        private readonly ChatGPT _chatGPT;
        private readonly Gemini _gemini;
        private readonly DeepSeek _deepSeek;

        public AIService(DataContext context, ChatGPT chatGPT, Gemini gemini, DeepSeek deepSeek)
        {
            _context = context;
            _chatGPT = chatGPT;
            _gemini = gemini;
            _deepSeek = deepSeek;
        }

        public async Task<string> AskAIAsync(int aiAgentId, string prompt)
        {
            var aiAgent = await _context.AIAgents.FindAsync(aiAgentId);
            if (aiAgent == null || !aiAgent.IsActive)
            {
                throw new InvalidOperationException("AIAgent ne postoji ili nije aktivan.");
            }

            // ✅ Kombinujemo RolePrompt i korisnikov Prompt
            string combinedPrompt = $"{aiAgent.RolePrompt}\n\n{prompt}";

            string response = string.Empty;
            bool isSuccess = true;
            string? errorMessage = null;

            try
            {
                switch (aiAgent.AIModel.ToString())
                {
                    case "ChatGPT":
                        response = await _chatGPT.GetResponseAsync(aiAgent.ApiKey, combinedPrompt);
                        break;
                    case "Gemini":
                        response = await _gemini.GetResponseAsync(aiAgent.ApiKey, combinedPrompt);
                        break;
                    case "DeepSeek":
                        response = await _deepSeek.GetResponseAsync(aiAgent.ApiKey, combinedPrompt);
                        break;
                    default:
                        throw new NotImplementedException($"Nepoznat AI model: {aiAgent.AIModel}");
                }
            }
            catch (Exception ex)
            {
                response = "Greška pri pozivanju AI servisa.";
                isSuccess = false;
                errorMessage = ex.Message;
            }

            // ✅ Upisujemo Usage podatke u bazu
            var usageLog = new AIAgentUsage
            {
                AIAgentId = aiAgent.Id,
                Prompt = combinedPrompt, // ✅ Sada upisujemo kombinovani prompt
                Response = response,
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage,
                RequestedAt = DateTime.UtcNow
            };

            _context.AIAgentUsages.Add(usageLog);
            await _context.SaveChangesAsync();

            return response;
        }
    }
}
