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
    public class ReportsController : ControllerBase
    {
        private readonly DataContext _context;

        public ReportsController(DataContext context)
        {
            _context = context;
        }

        // GET: /api/reports/monthly-sales
        [HttpGet("monthly-sales")]
        public async Task<IActionResult> GetMonthlySalesReport()
        {
            var report = await _context.Invoices
                .GroupBy(i => new { i.IssuedDate.Year, i.IssuedDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSales = g.Sum(i => i.TotalAmount),
                    TotalTax = g.Sum(i => i.TaxAmount)
                })
                .OrderByDescending(g => g.Year)
                .ThenByDescending(g => g.Month)
                .ToListAsync();

            return Ok(report);
        }

        // GET: /api/reports/annual-sales
        [HttpGet("annual-sales")]
        public async Task<IActionResult> GetAnnualSalesReport()
        {
            var report = await _context.Invoices
                .GroupBy(i => i.IssuedDate.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    TotalSales = g.Sum(i => i.TotalAmount),
                    TotalTax = g.Sum(i => i.TaxAmount)
                })
                .OrderByDescending(g => g.Year)
                .ToListAsync();

            return Ok(report);
        }

        // GET: /api/reports/unpaid-invoices
        [HttpGet("unpaid-invoices")]
        public async Task<IActionResult> GetUnpaidInvoices()
        {
            var unpaidInvoices = await _context.Invoices
                .Where(i => !i.IsPaid)
                .Select(i => new
                {
                    i.Id,
                    i.InvoiceNumber,
                    i.IssuedDate,
                    i.CustomerName,
                    i.TotalAmount
                })
                .ToListAsync();

            return Ok(unpaidInvoices);
        }

        // GET: /api/reports/tax-summary
        [HttpGet("tax-summary")]
        public async Task<IActionResult> GetTaxSummary()
        {
            var taxSummary = await _context.Invoices
                .Select(i => new
                {
                    Year = i.IssuedDate.Year,
                    Month = i.IssuedDate.Month,
                    NetAmount = i.NetAmount,
                    TaxAmount = i.TaxAmount,
                    TotalAmount = i.TotalAmount
                })
                .GroupBy(i => new { i.Year, i.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSales = g.Sum(i => i.TotalAmount),
                    TotalTax = g.Sum(i => i.TaxAmount)
                })
                .OrderByDescending(g => g.Year)
                .ThenByDescending(g => g.Month)
                .ToListAsync();

            return Ok(taxSummary);
        }
    }
}
