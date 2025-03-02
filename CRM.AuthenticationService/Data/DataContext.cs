using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CRM.AuthenticationService.Models;

namespace CRM.AuthenticationService.Data
{
    public class DataContext : IdentityDbContext<User> // Koristi User umesto IdentityUser
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
