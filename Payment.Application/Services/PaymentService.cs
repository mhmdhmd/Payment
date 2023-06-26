using AutoMapper;
using Microsoft.Extensions.Logging;
using Payment.Application.Interfaces;
using Payment.Application.Interfaces.Repositories;
using Payment.Application.Interfaces.Services;
using Payment.Application.ViewModels;
using Payment.Domain.DomainModels.Order;
using Payment.Domain.DomainModels.Payment;
using Payment.Domain.DomainModels.Shopper;

namespace Payment.Application.Services;

/// <summary>
/// Represents a payment service that handles payment-related operations.
/// </summary>
public class PaymentService : ServiceBase, IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<PaymentService> _logger;
    private readonly IPaymentProvider _paymentProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentService"/> class with the specified dependencies.
    /// </summary>
    /// <param name="providerFactory">The payment provider factory.</param>
    /// <param name="mapper">The IMapper instance used for object mapping.</param>
    /// <param name="paymentRepository">The payment repository.</param>
    /// <param name="logger">The logger instance used for logging.</param>
    public PaymentService(
        IPaymentProviderFactory providerFactory,
        IMapper mapper,
        IPaymentRepository paymentRepository,
        ILogger<PaymentService> logger)
        : base(mapper)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
        _paymentProvider = providerFactory.Create();
    }

    /// <inheritdoc />
    public async Task<PaymentResult> PayAsync(PaymentRequest paymentRequest)
    {
        #region Payment provider section (like Adyen)

        PaymentInfo paymentInfo = Mapper.Map<PaymentInfo>(paymentRequest);
        var response = await _paymentProvider.PayAsync(paymentInfo);
        _logger.LogInformation("PaymentService: Payment Done");

        #endregion

        #region Database section

        var shopper = Mapper.Map<ShopperEntity>(paymentRequest);
        await _paymentRepository.AddAsync(GetPayment(response, shopper));
        _logger.LogInformation("PaymentService: Payment information was inserted in the database");

        #endregion

        var result = new PaymentResult();
        if (response.Status == PaymentStatus.Paid)
        {
            result = Mapper.Map<PaymentResult>(response);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<PaymentHistoryResult> GetHistoryAsync()
    {
        var paymentHistory = await _paymentRepository.GetAllAsync(ph => ph.OrderEntity, ph => ph.OrderEntity.ShopperEntity);
        
        var result = Mapper.Map<PaymentHistoryResult>(paymentHistory);
        return result;
    }

    #region Private Methods

    /// <summary>
    /// Creates a payment history entity based on the payment response and shopper entity.
    /// </summary>
    /// <param name="paymentResponse">The payment response.</param>
    /// <param name="shopperEntity">The shopper entity.</param>
    /// <returns>The payment history entity.</returns>
    private PaymentHistoryEntity GetPayment(IPaymentResponse paymentResponse, ShopperEntity shopperEntity)
    {
        var orderDetails = new List<OrderDetailEntity>
        {
            new OrderDetailEntity
            {
                Price = 10M,
                ProductName = "Product 1"
            },
            new OrderDetailEntity
            {
                Price = paymentResponse.Amount - 10,
                ProductName = "Product 2"
            }
        };

        var order = new OrderEntity
        {
            OrderDetails = orderDetails,
            OrderDate = DateTime.Now,
            TotalPrice = paymentResponse.Amount,
            ShopperEntity = shopperEntity
        };

        var paymentHistory = new PaymentHistoryEntity
        {
            OrderEntity = order,
            PayDate = DateTime.Now,
            Status = (int)paymentResponse.Status,
            TransactionId = paymentResponse.TransactionId
        };

        return paymentHistory;
    }

    #endregion
}