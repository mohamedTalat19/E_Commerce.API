using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace E_Commerce.API.Controllers
{

    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly PaymentGetwaySettings _paymentGetwaySettings;

        public PaymentsController(IPaymentService paymentService, IOptions<PaymentGetwaySettings> options)
        {
            _paymentService = paymentService;
            _paymentGetwaySettings = options.Value;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentAsync(string basketId, CancellationToken ct)
        {
            return ToActionResult(await _paymentService.CreateOrUpdateIntentAsync(basketId, ct));
        }

        [HttpPost("webhook")]
        //Post baseUrl/api/Payments/webhook
        public async Task<IActionResult> StripeWebhook()
        {
            var requestJson = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var StripeEvent = EventUtility.ConstructEvent(requestJson,
                    Request.Headers["Stripe-Signature"],
                    _paymentGetwaySettings.WebhookSecret);

                switch (StripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:

                        var paymentIntent = StripeEvent.Data.Object as PaymentIntent;
                        if (paymentIntent is not null)
                            await _paymentService.PaymentSucceeded(paymentIntent.Id);
                        break;

                    case EventTypes.PaymentIntentPaymentFailed:
                        var paymentIntendFailed = StripeEvent.Data.Object as PaymentIntent;
                        if (paymentIntendFailed is not null)
                            await _paymentService.PaymentFailed(paymentIntendFailed.Id);
                        break;

                    default:
                        break;

                }
                return Ok();
            }
            catch(StripeException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
                
        }

    }
}
