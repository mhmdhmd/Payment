using Adyen.Service.Checkout;

namespace Payment.Infrastructure.Services.Payment.Adyen.Client;

public interface IAdyenClient
{
    /// <summary>
    /// Gets the payments service for handling payment requests.
    /// </summary>
    IPaymentsService Checkout { get; }
}