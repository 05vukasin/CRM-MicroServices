using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.PaymentService.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; } // Pozitivan za uplatu, negativan za isplatu

        [Required]
        [StringLength(3)]
        public string Currency { get; set; } = "USD"; // Default valuta

        [Required]
        public TransactionType Type { get; set; } // Deposit ili Withdrawal

        [Required]
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending; // Podrazumevani status

        public string? PaymentIntentId { get; set; } // ID Stripe transakcije (ako postoji)

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int PreviousBalance { get; set; } // Stanje pre transakcije

        [Required]
        public int CurrentBalance { get; set; } // Stanje posle transakcije
    }

    public enum TransactionType
    {
        Deposit,  // Uplata
        Withdrawal // Isplata
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed
    }
}
