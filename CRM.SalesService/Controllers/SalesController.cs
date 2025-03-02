using CRM.SalesService.Data;
using CRM.SalesService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.SalesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly DataContext _context;

        public SalesController(DataContext context)
        {
            _context = context;
        }

        // GET: /api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        // GET: /api/sales/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // POST: /api/sales
        [HttpPost]
        public async Task<ActionResult<Sale>> CreateSale(Sale sale)
        {
            // Proveravamo da li faktura postoji pre dodavanja prodaje
            var invoice = await _context.Invoices.FindAsync(sale.InvoiceId);
            if (invoice == null)
            {
                return BadRequest("Invoice not found.");
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }

        // PUT: /api/sales/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, Sale updatedSale)
        {
            if (id != updatedSale.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sales.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: /api/sales/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: /api/sales/by-invoice/{invoiceId}
        [HttpGet("by-invoice/{invoiceId}")]
        public async Task<ActionResult<Sale>> GetSaleByInvoice(int invoiceId)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.InvoiceId == invoiceId);
            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: /api/sales/{id}/mark-paid
        [HttpPut("{id}/mark-paid")]
        public async Task<IActionResult> MarkSaleAsPaid(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            sale.Status = "Completed";
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: /api/sales/{id}/refund
        [HttpPost("{id}/refund")]
        public async Task<IActionResult> RefundSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            if (sale.Status == "Refunded")
            {
                return BadRequest("Sale is already refunded.");
            }

            sale.Status = "Refunded";
            sale.TotalAmount = 0; // Povrat novca
            sale.TaxAmount = 0;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
