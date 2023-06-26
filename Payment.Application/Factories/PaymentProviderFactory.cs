using Microsoft.Extensions.Options;
using Payment.Application.Helper;
using Payment.Application.Interfaces;

namespace Payment.Application.Factories;

/// <summary>
/// Represents a factory for creating payment providers based on the provider name specified in the payment settings.
/// </summary>
public class PaymentProviderFactory : IPaymentProviderFactory
{
    private readonly Func<IEnumerable<IPaymentProvider>> _factory;
    private readonly PaymentSettings _paymentSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentProviderFactory"/> class.
    /// </summary>
    /// <param name="factory">The factory function that resolves a collection of payment providers.</param>
    /// <param name="paymentSettings">The payment settings injected as options.</param>
    public PaymentProviderFactory(Func<IEnumerable<IPaymentProvider>> factory, IOptions<PaymentSettings> paymentSettings)
    {
        _factory = factory;
        _paymentSettings = paymentSettings.Value;
    }

    /// <summary>
    /// Creates a payment provider based on the provider name specified in the payment settings.
    /// </summary>
    /// <returns>The created payment provider.</returns>
    /// <exception cref="NullReferenceException">Thrown when the payment provider is null. This can occur if the provider name is incorrect or no provider has been registered.</exception>
    public IPaymentProvider Create()
    {
        var paymentProviders = _factory();
        var paymentProviderName = _paymentSettings.Provider;
        try
        {
            var paymentProvider = paymentProviders.First(p => p.Name.Equals(paymentProviderName));
            return paymentProvider;
        }
        catch (Exception e)
        {
            throw new NullReferenceException("Payment Provider is null. Provider under PaymentSettings is incorrect or no provider has been registered", e.InnerException);
        }
    }
}