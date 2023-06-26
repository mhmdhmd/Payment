using Adyen.Model.Checkout;
using Payment.Application.ViewModels;
using Payment.Domain.DomainModels.Payment;
using Payment.Infrastructure.Services.Payment.Adyen.Adapter;

namespace Payment.Tests.Infrastructure.Adyen;

[TestFixture]
public class AdyenPaymentAdapterTests
{
    private PaymentResponse _paymentResponse;

    [SetUp]
    public void Setup()
    {
        _paymentResponse = new PaymentResponse();
    }
    
    [Test]
    public void TransactionId_Should_Return_CorrectValue()
    {
        // Arrange
        _paymentResponse.PspReference = "12345";
        var adapter = new AdyenPaymentAdapter(_paymentResponse);

        // Act
        var result = adapter.TransactionId;

        // Assert
        Assert.That(result, Is.EqualTo("12345"));
    }
    
    [Test]
    public void Amount_Should_Return_CorrectValue()
    {
        // Arrange
        _paymentResponse.Amount = new Amount("USD", 1000);
        var adapter = new AdyenPaymentAdapter(_paymentResponse);

        // Act
        var result = adapter.Amount;

        // Assert
        Assert.That(result, Is.EqualTo(10.00m));
    }

    [Test]
    public void Currency_Should_Return_CorrectValue()
    {
        // Arrange
        _paymentResponse.Amount = new Amount("USD", 1000);
        var adapter = new AdyenPaymentAdapter(_paymentResponse);

        // Act
        var result = adapter.Currency;

        // Assert
        Assert.That(result, Is.EqualTo("USD"));
    }

    [TestCase(PaymentResponse.ResultCodeEnum.Authorised, PaymentStatus.Paid)]
    [TestCase(PaymentResponse.ResultCodeEnum.Pending, PaymentStatus.Paid)]
    [TestCase(PaymentResponse.ResultCodeEnum.Received, PaymentStatus.Paid)]
    [TestCase(PaymentResponse.ResultCodeEnum.Success, PaymentStatus.Paid)]
    [TestCase(PaymentResponse.ResultCodeEnum.Cancelled, PaymentStatus.Failed)]
    [TestCase(PaymentResponse.ResultCodeEnum.Error, PaymentStatus.Failed)]
    [TestCase(PaymentResponse.ResultCodeEnum.Refused, PaymentStatus.Failed)]
    public void Status_Should_Return_CorrectValue(PaymentResponse.ResultCodeEnum resultCode, PaymentStatus expectedStatus)
    {
        // Arrange
        _paymentResponse.ResultCode = resultCode;
        var adapter = new AdyenPaymentAdapter(_paymentResponse);

        // Act
        var result = adapter.Status;

        // Assert
        Assert.That(result, Is.EqualTo(expectedStatus));
    }

    [Test]
    public void ResultType_WithNonNullResultCode_Should_Return_CorrectValue()
    {
        // Arrange
        _paymentResponse.ResultCode = PaymentResponse.ResultCodeEnum.Authorised;
        var adapter = new AdyenPaymentAdapter(_paymentResponse);

        // Act
        var result = adapter.ResultType;

        // Assert
        Assert.That(result, Is.EqualTo("Authorised"));
    }

    [Test]
    public void ResultType_WithNullResultCode_Should_Return_Unknown()
    {
        // Arrange
        _paymentResponse.ResultCode = null;
        var adapter = new AdyenPaymentAdapter(_paymentResponse);

        // Act
        var result = adapter.ResultType;

        // Assert
        Assert.That(result, Is.EqualTo("Unknown"));
    }
}