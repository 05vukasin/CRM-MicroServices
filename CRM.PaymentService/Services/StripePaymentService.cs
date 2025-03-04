using System;
using System.Threading.Tasks;
using Stripe;

namespace CRM.PaymentService.Services
{
    public class StripePaymentService
    {
        public StripePaymentService(string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;
        }

        // ✅ Kreiranje plaćanja (Stripe PaymentIntent)
        public async Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe koristi centove
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }

        // ✅ Dohvatanje statusa plaćanja
        public async Task<PaymentIntent> RetrievePaymentIntent(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            return await service.GetAsync(paymentIntentId);
        }
    }
}
