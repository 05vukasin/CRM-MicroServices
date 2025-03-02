using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.SalesService.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; } // Veza sa fakturom

        public DateTime SaleDate { get; set; } // Datum prodaje
        public decimal TotalAmount { get; set; } // Ukupan iznos sa PDV-om
        public decimal TaxAmount { get; set; } // Iznos PDV-a
        public string PaymentMethod { get; set; } // Gotovina, Kartica, Bankovni transfer
        public string Status { get; set; } // "Completed", "Refunded", "Pending"

        // Navigaciono svojstvo ka fakturi
        public Invoice Invoice { get; set; }
    }
}
