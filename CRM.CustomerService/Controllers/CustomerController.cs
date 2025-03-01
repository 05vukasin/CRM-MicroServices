using CRM.CustomerService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;
        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Models.Customer>> PostCustomer(DTOs.PostCustomer postCustomer)
        {
            var customer = new Models.Customer
            {
                Name = postCustomer.Name,
                Email = postCustomer.Email,
                Address = postCustomer.Address,
                Phone = postCustomer.Phone
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok("Created Sucessfuly ");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, DTOs.PostCustomer postCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = postCustomer.Name;
            customer.Email = postCustomer.Email;
            customer.Address = postCustomer.Address;
            customer.Phone = postCustomer.Phone;

            await _context.SaveChangesAsync();

            return Ok("Updated Sucsesfuly");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("Deleted Sucsesfuly");
        }
    }
}
