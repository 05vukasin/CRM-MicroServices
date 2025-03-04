using CRM.PaymentService.Data;
using CRM.PaymentService.DTOs;
using CRM.PaymentService.Models;
using CRM.PaymentService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly StripePaymentService _stripePaymentService;

        public TransactionController(DataContext context, StripePaymentService stripePaymentService)
        {
            _context = context;
            _stripePaymentService = stripePaymentService;
        }

        // ✅ Kreiranje uplate (Deposit)
        [HttpPost("deposit")]
        public async Task<IActionResult> CreateDeposit([FromBody] DepositRequestDTO depositRequest)
        {
            if (depositRequest.Amount <= 0)
                return BadRequest("Iznos mora biti veći od 0.");

            try
            {
                // ✅ Pronalaženje poslednjeg stanja korisnika (u centima)
                int lastBalance = _context.Transactions.OrderByDescending(t => t.Id)
                                    .Select(t => t.CurrentBalance).FirstOrDefault();

                var paymentIntent = await _stripePaymentService.CreatePaymentIntent(depositRequest.Amount, depositRequest.Currency);

                var transaction = new Transaction
                {
                    Amount = depositRequest.Amount,
                    Currency = depositRequest.Currency,
                    PaymentIntentId = paymentIntent.Id,
                    Type = TransactionType.Deposit,
                    Status = TransactionStatus.Pending,
                    PreviousBalance = lastBalance,
                    CurrentBalance = lastBalance + depositRequest.Amount, // Dodajemo iznos u centima
                    CreatedAt = DateTime.UtcNow
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return Ok(new TransactionResponseDTO
                {
                    TransactionId = transaction.Id,
                    Status = transaction.Status.ToString(),
                    ClientSecret = paymentIntent.ClientSecret
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Greška prilikom kreiranja transakcije: {ex.Message}");
            }
        }

        // ✅ Kreiranje isplate (Withdrawal)
        [HttpPost("withdraw")]
        public async Task<IActionResult> CreateWithdrawal([FromBody] WithdrawalRequestDTO withdrawalRequest)
        {
            if (withdrawalRequest.Amount <= 0)
                return BadRequest("Iznos mora biti veći od 0.");

            try
            {
                // ✅ Pronalaženje poslednjeg stanja korisnika (u centima)
                int lastBalance = _context.Transactions.OrderByDescending(t => t.Id)
                                    .Select(t => t.CurrentBalance).FirstOrDefault();

                if (lastBalance < withdrawalRequest.Amount)
                    return BadRequest("Nedovoljno sredstava za isplatu.");

                var transaction = new Transaction
                {
                    Amount = withdrawalRequest.Amount,
                    Currency = withdrawalRequest.Currency,
                    Type = TransactionType.Withdrawal,
                    Status = TransactionStatus.Pending,
                    PreviousBalance = lastBalance,
                    CurrentBalance = lastBalance - withdrawalRequest.Amount, // Oduzimamo iznos u centima
                    CreatedAt = DateTime.UtcNow
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return Ok(new TransactionResponseDTO
                {
                    TransactionId = transaction.Id,
                    Status = transaction.Status.ToString()
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Greška prilikom kreiranja isplate: {ex.Message}");
            }
        }

        // ✅ Provera statusa transakcije
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionStatus(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
                return NotFound("Transakcija nije pronađena.");

            if (!string.IsNullOrEmpty(transaction.PaymentIntentId))
            {
                var paymentIntent = await _stripePaymentService.RetrievePaymentIntent(transaction.PaymentIntentId);
                transaction.Status = paymentIntent.Status switch
                {
                    "succeeded" => TransactionStatus.Completed,
                    "canceled" => TransactionStatus.Failed,
                    _ => TransactionStatus.Pending
                };

                await _context.SaveChangesAsync();
            }

            return Ok(new TransactionStatusDTO
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                PreviousBalance = transaction.PreviousBalance,
                CurrentBalance = transaction.CurrentBalance,
                Type = transaction.Type,
                Status = transaction.Status,
                CreatedAt = transaction.CreatedAt
            });
        }
    }
}
