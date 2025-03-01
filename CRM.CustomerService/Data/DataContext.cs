using Microsoft.EntityFrameworkCore;

namespace CRM.CustomerService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Models.Customer> Customers { get; set; }
    }
}
