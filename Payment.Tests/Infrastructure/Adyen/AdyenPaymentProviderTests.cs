using Adyen.HttpClient;
using Adyen.Model.Checkout;
using Adyen.Service.Checkout;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Payment.Application.Helper;
using Payment.Application.Interfaces;
using Payment.Application.ViewModels;
using Payment.Infrastructure.Exceptions;
using Payment.Infrastructure.Services.Payment.Adyen;
using Payment.Infrastructure.Services.Payment.Adyen.Client;
using Card = Payment.Application.ViewModels.Card;
using PaymentRequest = Adyen.Model.Checkout.PaymentRequest;

namespace Payment.Tests.Infrastructure.Adyen;

[TestFixture]
public class AdyenPaymentProviderTests
{
    private PaymentInfo _paymentInfo;
    private IOptions<PaymentSettings> _options;
    private ILogger<AdyenPaymentProvider> _logger;
    
    [SetUp]
    public void Setup()
    {
        _paymentInfo = new PaymentInfo
        {
            CurrencyType = CurrencyType.USD,
            Amount = 10.5m,
            Description = "Test payment",
            Card = new Card
            {
                EncryptedCardNumber = "encryptedNumber",
                EncryptedExpiryMonth = "encryptedMonth",
                EncryptedExpiryYear = "encryptedYear",
                EncryptedSecurityCode = "encryptedCode"
            }
        };
        
        var paymentSettings = new PaymentSettings
        {
            AdyenSettings = new AdyenSettings
            {
                MerchantAccount = "testMerchantAccount"
            }
        };
        
        _options = Options.Create(paymentSettings);
        _logger = Substitute.For<ILogger<AdyenPaymentProvider>>();
    }
    
    [Test]
    public async Task Pay_ValidPaymentInfo_ReturnsPaymentResponse2()
    {
        // Arrange
        var adyenClient = Substitute.For<IAdyenClient>();
        var paymentsService = Substitute.For<IPaymentsService>();
        adyenClient.Checkout.Returns(paymentsService);
        adyenClient.Checkout.Payments(Arg.Any<PaymentRequest>()).Returns(new PaymentResponse());
        
        var paymentProvider = new AdyenPaymentProvider(adyenClient, _options, _logger);
        
        // Act
        var result = await paymentProvider.PayAsync(_paymentInfo);
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IPaymentResponse>(result);
    }
    
    [Test]
    public void Pay_ThrowsPaymentException_ThrowsException()
    {
        // Arrange
        var adyenClient = Substitute.For<IAdyenClient>();
        var paymentsService = Substitute.For<IPaymentsService>();
        adyenClient.Checkout.Returns(paymentsService);
        adyenClient.Checkout.PaymentsAsync(Arg.Any<PaymentRequest>()).Throws(new HttpClientException(0, "http client exception", "some body"));
        
        var paymentProvider = new AdyenPaymentProvider(adyenClient, _options, _logger);

        // Act & Assert
        Assert.ThrowsAsync<PaymentException>(() => paymentProvider.PayAsync(_paymentInfo));
    }
}