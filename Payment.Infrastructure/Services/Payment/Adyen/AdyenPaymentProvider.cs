using Adyen.HttpClient;
using Adyen.Model.Checkout;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payment.Application.Extensions;
using Payment.Application.Helper;
using Payment.Application.Interfaces;
using Payment.Application.ViewModels;
using Payment.Domain.DomainModels.Payment;
using Payment.Infrastructure.Exceptions;
using Payment.Infrastructure.Services.Payment.Adyen.Adapter;
using Payment.Infrastructure.Services.Payment.Adyen.Client;
using Payment.Infrastructure.Services.Payment.Adyen.Helper;
using PaymentRequest = Adyen.Model.Checkout.PaymentRequest;

namespace Payment.Infrastructure.Services.Payment.Adyen;

/// <summary>
/// Implementation of the Adyen payment provider.
/// </summary>
public class AdyenPaymentProvider : IPaymentProvider
{
    private readonly IAdyenClient _client;
    private readonly ILogger<AdyenPaymentProvider> _logger;
    private readonly PaymentSettings _paymentSettings;

    /// <summary>
    /// Gets the name of the payment provider. It is used in PaymentProviderFactory (Factory Pattern) to retrieve the selected payment provider in appsettings.json by PaymentSettings.Provider 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdyenPaymentProvider"/> class.
    /// </summary>
    /// <param name="client">The Adyen client for making API requests.</param>
    /// <param name="paymentSettings">The payment settings for configuring the provider.</param>
    /// <param name="logger">The logger for logging payment provider activities.</param>
    public AdyenPaymentProvider(IAdyenClient client, IOptions<PaymentSettings> paymentSettings, ILogger<AdyenPaymentProvider> logger)
    {
        _client = client;
        _logger = logger;
        _paymentSettings = paymentSettings.Value;
        Name = "Adyen";
    }

    /// <summary>
    /// Makes a payment using the Adyen payment provider.
    /// </summary>
    /// <param name="payment">The payment information.</param>
    /// <returns>The payment response.</returns>
    public async Task<IPaymentResponse> PayAsync(PaymentInfo payment)
    {
        #region Initialize request for Adyen Payments method

        var currency = payment.CurrencyType.GetDescription();
        var amount = new Amount(currency, CurrencyConverter.ToMinorUnits(payment.Amount, currency));

        var paymentMethod = new CardDetails
        {
            Type = CardDetails.TypeEnum.Scheme,
            EncryptedCardNumber = payment.Card.EncryptedCardNumber,
            EncryptedExpiryMonth = payment.Card.EncryptedExpiryMonth,
            EncryptedExpiryYear = payment.Card.EncryptedExpiryYear,
            EncryptedSecurityCode = payment.Card.EncryptedSecurityCode
        };

        var paymentRequest = new PaymentRequest
        {
            Reference = payment.Description,
            Amount = amount,
            MerchantAccount = _paymentSettings.AdyenSettings.MerchantAccount,
            PaymentMethod = new CheckoutPaymentMethod(paymentMethod)
        };

        #endregion

        try
        {
            var response = await _client.Checkout.PaymentsAsync(paymentRequest);
            _logger.LogInformation("AdyenPaymentProvider: Payment Done");
            return new AdyenPaymentAdapter(response);
        }
        catch (HttpClientException e)
        {
            throw new PaymentException(e.Message);
        }
    }
}