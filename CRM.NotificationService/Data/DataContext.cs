using Microsoft.EntityFrameworkCore;

namespace CRM.NotificationService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Models.GmailAgent> GmailAgents { get; set; }
        public DbSet<Models.GmailAgentUsage> GmailAgentUsages { get; set; }
    }
}
