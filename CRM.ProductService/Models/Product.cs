namespace CRM.ProductService.Models
{
    public class Product
    {
        public int Id { get; set; } // Jedinstveni identifikator proizvoda
        public string Name { get; set; } = string.Empty; // Naziv proizvoda
        public string Description { get; set; } = string.Empty; // Opis proizvoda
        public decimal Price { get; set; } // Cena proizvoda
        public int StockQuantity { get; set; } // Količina na stanju
        public string SKU { get; set; } = string.Empty; // Šifra proizvoda
        public string Category { get; set; } = string.Empty; // Kategorija proizvoda
        public string ImageUrl { get; set; } = string.Empty; // URL slike proizvoda
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Datum kreiranja proizvoda
        public DateTime? UpdatedAt { get; set; } // Datum poslednje izmene proizvoda
        public bool IsActive { get; set; } = true; // Da li je proizvod aktivan
        public string Supplier { get; set; } = string.Empty; // Dobavljač proizvoda
    }
}
