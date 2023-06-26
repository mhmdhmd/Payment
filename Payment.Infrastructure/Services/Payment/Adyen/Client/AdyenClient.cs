using Adyen;
using Adyen.Service.Checkout;
using Microsoft.Extensions.Options;
using Payment.Application.Helper;
using Environment = Adyen.Model.Environment;

namespace Payment.Infrastructure.Services.Payment.Adyen.Client;

/// <summary>
/// Adyen client implementation for making payments.
/// </summary>
public class AdyenClient : IAdyenClient
{
    /// <inheritdoc />
    public IPaymentsService Checkout { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdyenClient"/> class.
    /// </summary>
    /// <param name="paymentSettings">The payment settings.</param>
    public AdyenClient(IOptions<PaymentSettings> paymentSettings)
    {
        var settings = paymentSettings.Value;

        var config = new Config
        {
            XApiKey = settings.AdyenSettings.ApiKey,
            Environment = Environment.Test
        };
        var client = new global::Adyen.Client(config);
        Checkout = new PaymentsService(client);
    }
}