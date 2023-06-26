using Payment.Application.ViewModels;

namespace Payment.Application.Interfaces.Services;

/// <summary>
/// Represents a payment service.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Processes a payment request.
    /// </summary>
    /// <param name="paymentRequest">The payment request.</param>
    /// <returns>The payment result.</returns>
    Task<PaymentResult> PayAsync(PaymentRequest paymentRequest);

    /// <summary>
    /// Retrieves the payment history.
    /// </summary>
    /// <returns>The payment history result.</returns>
    Task<PaymentHistoryResult> GetHistoryAsync();
}