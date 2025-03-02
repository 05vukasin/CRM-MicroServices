using CRM.SalesService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.SalesService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Invoice)
                .WithOne(i => i.Sale)
                .HasForeignKey<Sale>(s => s.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade); // Ako se briše faktura, briše se i prodaja

            base.OnModelCreating(modelBuilder);
        }
    }
}
