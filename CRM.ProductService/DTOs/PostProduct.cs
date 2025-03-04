namespace CRM.ProductService.DTOs
{
    public class PostProduct
    {
        public string Name { get; set; } = string.Empty; // Naziv proizvoda
        public string Description { get; set; } = string.Empty; // Opis proizvoda
        public decimal Price { get; set; } // Cena proizvoda
        public int StockQuantity { get; set; } // Količina na stanju
        public string SKU { get; set; } = string.Empty; // Šifra proizvoda
        public string Category { get; set; } = string.Empty; // Kategorija proizvoda
        public bool IsActive { get; set; } = true; // Da li je proizvod aktivan
        public string Supplier { get; set; } = string.Empty; // Dobavljač proizvoda
    }
}
