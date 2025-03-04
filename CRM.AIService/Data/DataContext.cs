using CRM.AIService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.AIService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AIAgent> AIAgents { get; set; }
        public DbSet<AIAgentUsage> AIAgentUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AIAgent>()
                .Property(a => a.AIModel)
                .HasConversion<string>(); // Ovo čuva enum kao string umesto integera
        }
    }
}
