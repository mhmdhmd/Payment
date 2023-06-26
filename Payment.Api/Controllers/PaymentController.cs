using Microsoft.AspNetCore.Mvc;
using Payment.Application.Interfaces.Services;
using Payment.Application.ViewModels;

namespace Payment.Api.Controllers
{
    /// <summary>
    /// Controller for handling payment-related actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        /// <param name="paymentService">The payment service to be used.</param>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Performs a payment.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns>The payment result <see cref="PaymentResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> PayAsync(PaymentRequest paymentRequest)
        {
            var result = await _paymentService.PayAsync(paymentRequest);

            return Ok(result);
        }

        /// <summary>
        /// Gets the payment history.
        /// </summary>
        /// <returns>The payment history result <see cref="PaymentHistoryResult"/>.</returns>
        [HttpGet("get-history")]
        public async Task<IActionResult> GetHistoryAsync()
        {
            var result = await _paymentService.GetHistoryAsync();
            
            return Ok(result);
        }
    }
}
