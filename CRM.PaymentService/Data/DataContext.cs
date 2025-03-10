﻿using Microsoft.EntityFrameworkCore;

namespace CRM.PaymentService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Models.Transaction> Transactions { get; set; }
    }
}
