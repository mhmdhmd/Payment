using Payment.Application.ViewModels;

namespace Payment.Application.Interfaces;

/// <summary>
/// Represents a common payment response object used by payment providers.
/// </summary>
public interface IPaymentResponse
{
    string TransactionId { get; }
    decimal Amount { get; }
    string Currency { get; }
    PaymentStatus Status { get; }
    string ResultType { get; }
}