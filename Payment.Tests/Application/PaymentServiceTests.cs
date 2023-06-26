using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Payment.Application.Interfaces;
using Payment.Application.Interfaces.Repositories;
using Payment.Application.Services;
using Payment.Application.ViewModels;
using Payment.Domain.DomainModels.Payment;
using Payment.Domain.DomainModels.Shopper;

namespace Payment.Tests.Application;

[TestFixture]
public class PaymentServiceTests
{
    private IPaymentHistoryRepository _paymentHistoryRepository;
    private IPaymentProviderFactory _providerFactory;
    private IMapper _mapper;
    private PaymentService _paymentService;
    private IPaymentProvider _paymentProvider;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        _mapper = Substitute.For<IMapper>();
        _paymentHistoryRepository = Substitute.For<IPaymentHistoryRepository>();
        _paymentProvider = Substitute.For<IPaymentProvider>();
        _providerFactory = Substitute.For<IPaymentProviderFactory>();
        _providerFactory.Create().Returns(_paymentProvider);
        var _logger = Substitute.For<ILogger<PaymentService>>();
        _paymentService = new PaymentService(_providerFactory, _mapper, _paymentHistoryRepository, _logger);
    }

    [Test]
    public async Task Pay_ShouldMapPaymentRequestToPaymentInfo()
    {
        // Arrange
        var paymentRequest = new PaymentRequest();

        // Act
        await _paymentService.PayAsync(paymentRequest);

        // Assert
        _mapper.Received(1).Map<PaymentInfo>(paymentRequest);
    }
    
    [Test]
    public async Task Pay_ShouldMapPaymentRequestToShopper()
    {
        // Arrange
        var paymentRequest = new PaymentRequest();

        // Act
        await _paymentService.PayAsync(paymentRequest);

        // Assert
        _mapper.Received(1).Map<ShopperEntity>(paymentRequest);
    }

    [Test]
    public async Task Pay_ShouldCallPayMethodOfPaymentProviderWithPaymentInfo()
    {
        // Arrange
        var paymentRequest = new PaymentRequest();
        var paymentInfo = new PaymentInfo();
        _mapper.Map<PaymentInfo>(paymentRequest).Returns(paymentInfo);

        // Act
        await _paymentService.PayAsync(paymentRequest);

        // Assert
        await _paymentProvider.Received(1).PayAsync(paymentInfo);
    }

    [Test]
    public async Task Pay_ShouldReturnEmptyPaymentResult_IfPaymentStatusIsFailed()
    {
        // Arrange
        var paymentResponse = Substitute.For<IPaymentResponse>();
        paymentResponse.Status.Returns(PaymentStatus.Failed);
        _paymentProvider.PayAsync(Arg.Any<PaymentInfo>()).Returns(paymentResponse);

        // Act
        var result = await _paymentService.PayAsync(Arg.Any<PaymentRequest>());

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<PaymentResult>(result);
        Assert.IsNull(result.Data);
        Assert.IsFalse(result.Success);
    }

    [Test]
    public async Task Pay_ShouldMapPaymentResponseToPaymentResult_IfPaymentStatusIsPaid()
    {
        // Arrange
        var paymentResult = new PaymentResult
        {
            Success = true,
            Data = new()
        };
        
        _mapper.Map<PaymentResult>(Arg.Any<IPaymentResponse>()).Returns(paymentResult);

        var paymentResponse = Substitute.For<IPaymentResponse>();
        paymentResponse.Status.Returns(PaymentStatus.Paid);
        _paymentProvider.PayAsync(Arg.Any<PaymentInfo>()).Returns(paymentResponse);

        // Act
        var result = await _paymentService.PayAsync(Arg.Any<PaymentRequest>());

        // Assert
        Assert.IsInstanceOf<PaymentResult>(result);
        Assert.IsNotNull(result.Data);
        Assert.IsTrue(result.Success);
    }
    
    [Test]
    public async Task GetHistory_ReturnsPaymentHistoryResult()
    {
        // Arrange
        var paymentHistory = new List<PaymentHistoryEntity>();
        var mappedResult = new PaymentHistoryResult();

        _paymentHistoryRepository.GetAllAsync(Arg.Any<Expression<Func<PaymentHistoryEntity, object>>>(),
            Arg.Any<Expression<Func<PaymentHistoryEntity, object>>>()).Returns(paymentHistory);
        _mapper.Map<PaymentHistoryResult>(paymentHistory).Returns(mappedResult);

        // Act
        var result = await _paymentService.GetHistoryAsync();
        
        // Assert
        Assert.That(result, Is.EqualTo(mappedResult));
    }
}