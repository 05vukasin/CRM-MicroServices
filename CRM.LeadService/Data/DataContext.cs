using Microsoft.EntityFrameworkCore;

namespace CRM.LeadService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Models.Lead> Leads { get; set; }
    }
}
