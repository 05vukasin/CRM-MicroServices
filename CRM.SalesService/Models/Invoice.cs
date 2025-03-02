using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.SalesService.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string InvoiceNumber { get; set; } // Broj fakture (generisan automatski)

        public DateTime IssuedDate { get; set; } // Datum izdavanja

        [Required]
        public string CustomerName { get; set; } // Ime kupca (za poresku evidenciju)

        [Required]
        public string CustomerTaxId { get; set; } // PIB kupca (ako je firma)

        public decimal NetAmount { get; set; } // Osnovica (bez PDV-a)
        public decimal TaxAmount { get; set; } // Iznos PDV-a
        public decimal TotalAmount { get; set; } // Ukupan iznos (sa PDV-om)
        public bool IsPaid { get; set; } // Da li je faktura plaćena

        // Navigaciono svojstvo ka prodaji (1:1)
        public Sale Sale { get; set; }
    }
}
