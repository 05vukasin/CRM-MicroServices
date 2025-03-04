using CRM.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.ProductService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
