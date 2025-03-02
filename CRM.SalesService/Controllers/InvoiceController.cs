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
    public class InvoiceController : ControllerBase
    {
        private readonly DataContext _context;

        public InvoiceController(DataContext context)
        {
            _context = context;
        }

        // GET: /api/invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        // GET: /api/invoices/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // POST: /api/invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        // PUT: /api/invoices/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, Invoice updatedInvoice)
        {
            if (id != updatedInvoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Invoices.Any(e => e.Id == id))
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

        // DELETE: /api/invoices/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: /api/invoices/tax-report
        [HttpGet("tax-report")]
        public async Task<IActionResult> GetTaxReport()
        {
            var taxSummary = await _context.Invoices
                .GroupBy(i => i.IssuedDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSales = g.Sum(i => i.TotalAmount),
                    TotalTax = g.Sum(i => i.TaxAmount)
                })
                .ToListAsync();

            return Ok(taxSummary);
        }

        // GET: /api/invoices/download/{id}
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            // Placeholder logika za preuzimanje PDF-a (može se implementirati kasnije)
            var fileContent = System.Text.Encoding.UTF8.GetBytes($"Faktura: {invoice.InvoiceNumber}, Ukupno: {invoice.TotalAmount}");
            var fileName = $"Invoice_{invoice.InvoiceNumber}.pdf";

            return File(fileContent, "application/pdf", fileName);
        }
    }
}
