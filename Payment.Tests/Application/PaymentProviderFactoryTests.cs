using Microsoft.Extensions.Options;
using NSubstitute;
using Payment.Application.Factories;
using Payment.Application.Helper;
using Payment.Application.Interfaces;

namespace Payment.Tests.Application;

[TestFixture]
public class PaymentProviderFactoryTests
{
    [Test]
    public void Create_ShouldReturnCorrectPaymentProvider()
    {
        // Arrange
        var factory = Substitute.For<Func<IEnumerable<IPaymentProvider>>>();
        var paymentSettings = new PaymentSettings { Provider = "Test2" };
        var options = Options.Create(paymentSettings);
        
        var provider1 = Substitute.For<IPaymentProvider>();
        provider1.Name.Returns("Test1");
        var provider2 = Substitute.For<IPaymentProvider>();
        provider2.Name.Returns("Test2");
        var provider3 = Substitute.For<IPaymentProvider>();
        provider3.Name.Returns("Test3");
        
        var paymentProviders = new List<IPaymentProvider> { provider1, provider2, provider3 };
        factory().Returns(paymentProviders);

        var paymentProviderFactory = new PaymentProviderFactory(factory, options);

        // Act
        var result = paymentProviderFactory.Create();

        // Assert
        Assert.That(result, Is.SameAs(provider2));
    }

    [Test]
    public void Create_WhenNoMatchingPaymentProviderFound_ThrowsNullReferenceException()
    {
        // Arrange
        var factory = Substitute.For<Func<IEnumerable<IPaymentProvider>>>();
        var provider1 = Substitute.For<IPaymentProvider>();
        var provider2 = Substitute.For<IPaymentProvider>();
        var paymentProviders = new List<IPaymentProvider> { provider1, provider2 };

        factory().Returns(paymentProviders);
        var options = Options.Create(new PaymentSettings { Provider = "Invalid Name" });
        var paymentProviderFactory = new PaymentProviderFactory(factory, options);
        
        // Act and Assert
        Assert.Throws<NullReferenceException>(() => paymentProviderFactory.Create());
    }
}