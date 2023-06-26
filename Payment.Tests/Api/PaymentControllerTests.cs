using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Payment.Api.Controllers;
using Payment.Application.Interfaces.Services;
using Payment.Application.ViewModels;

namespace Payment.Tests.Api;

[TestFixture]
public class PaymentControllerTests
{
    private IPaymentService _paymentService;
    private PaymentController _controller;

    [SetUp]
    public void Setup()
    {
        _paymentService = Substitute.For<IPaymentService>();
        _controller = new PaymentController(_paymentService);
    }

    [Test]
    public async Task Pay_ValidPaymentRequest_ReturnOkResult()
    {
        // Arrange
        var paymentRequest = new PaymentRequest
        {
            Currency = "EUR",
            Amount = 10.5M,
            Description = "Test for Ok result",
            CardNumber = "Card Number",
            ExpiryMonth = "Month",
            ExpiryYear = "Year",
            SecurityCode = "Code"
        };

        _paymentService.PayAsync(paymentRequest).Returns(new PaymentResult
        {
            Data = new PaymentResultModel
            {
                Currency = "EUR",
                Amount = 10.5M,
                TransactionId = "Some Transaction Id",
                Status = "Paid",
                ResultType = "Authorised"
            },
            Success = true,
            ErrorMessage = null
        });
        
        // Act
        var result = await _controller.PayAsync(paymentRequest) as OkObjectResult;
        var resultValue = result?.Value as PaymentResult;
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(resultValue?.Success);
        Assert.That(result?.StatusCode, Is.EqualTo(200));
        Assert.IsNotNull(result?.Value);
        Assert.IsInstanceOf<PaymentResult>(result?.Value);
    }
}