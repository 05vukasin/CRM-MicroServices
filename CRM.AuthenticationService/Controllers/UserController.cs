using CRM.AuthenticationService.Data;
using CRM.AuthenticationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM.AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;

        public UserController(UserManager<User> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // 📌 1️⃣ Endpoint: Listanje svih korisnika sa ulogama
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var result = users.Select(user => new
            {
                user.Id,
                user.Email,
                Role = user.Role ?? "No Role" // Direktno koristimo Role iz User
            }).ToList();

            return Ok(result);
        }

        // 🔒 2️⃣ Endpoint: Promena lozinke
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Password changed successfully" });
        }

        // 🎭 3️⃣ Endpoint: Dodela role korisniku
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            user.Role = request.Role; // Direktno ažuriramo Role u User modelu
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = $"Role '{request.Role}' assigned to user '{user.UserName}'" });
        }

        // 🔐 4️⃣ Endpoint: Dohvatanje podataka o trenutno prijavljenom korisniku
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User not authorized" });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(new
            {
                user.Id,
                user.Email,
                Role = user.Role ?? "No Role"
            });
        }
    }

    // DTO klasa za promenu lozinke
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    // DTO klasa za dodelu role
    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
