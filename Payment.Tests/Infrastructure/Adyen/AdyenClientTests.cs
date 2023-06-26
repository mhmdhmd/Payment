using Adyen.Service.Checkout;
using Microsoft.Extensions.Options;
using Payment.Application.Helper;
using Payment.Infrastructure.Services.Payment.Adyen.Client;

namespace Payment.Tests.Infrastructure.Adyen;

[TestFixture]
public class AdyenClientTests
{
    [Test]
    public void AdyenClient_WithValidPaymentSettings_CreatesPaymentsService()
    {
        // Arrange
        var paymentSettings = Options.Create(new PaymentSettings
        {
            Provider = "Adyen",
            AdyenSettings = new AdyenSettings
            {
                ApiKey = "your-api-key",
                MerchantAccount = "your-merchant-account"
            }
        });
        
        // Act
        var adyenClient = new AdyenClient(paymentSettings);

        // Assert
        Assert.IsNotNull(adyenClient.Checkout);
        Assert.IsInstanceOf<IPaymentsService>(adyenClient.Checkout);
    }
}