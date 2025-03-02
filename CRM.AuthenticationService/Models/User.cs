using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.AuthenticationService.Models
{
    public class User:IdentityUser
    {
        public string? Role { get; set; } // Uloga korisnika (Admin, User itd.)
    }
}
