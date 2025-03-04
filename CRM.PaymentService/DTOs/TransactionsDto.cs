using CRM.PaymentService.Models;

namespace CRM.PaymentService.DTOs
{
    // ✅ DTO za kreiranje depozita
    public class DepositRequestDTO
    {
        public int Amount { get; set; } // Iznos u centima
        public string Currency { get; set; } = "USD";
    }

    // ✅ DTO za kreiranje isplate
    public class WithdrawalRequestDTO
    {
        public int Amount { get; set; } // Iznos u centima
        public string Currency { get; set; } = "USD";
    }

    // ✅ DTO za odgovor nakon kreiranja transakcije (Deposit / Withdrawal)
    public class TransactionResponseDTO
    {
        public int TransactionId { get; set; }
        public string Status { get; set; }
        public string? ClientSecret { get; set; } // Za Stripe plaćanja
    }

    // ✅ DTO za prikaz statusa transakcije
    public class TransactionStatusDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int PreviousBalance { get; set; }
        public int CurrentBalance { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}


