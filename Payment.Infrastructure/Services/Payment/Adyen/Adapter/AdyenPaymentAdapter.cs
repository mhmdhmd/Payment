using Adyen.Model.Checkout;
using Payment.Application.Extensions;
using Payment.Application.Interfaces;
using Payment.Application.ViewModels;
using Payment.Domain.DomainModels.Payment;
using Payment.Infrastructure.Services.Payment.Adyen.Helper;

namespace Payment.Infrastructure.Services.Payment.Adyen.Adapter;

/// <summary>
/// Adapter class for adapting Adyen payment response to the IPaymentResponse (the common payment response in our domain) interface.
/// </summary>
public class AdyenPaymentAdapter : IPaymentResponse
{
    private readonly PaymentResponse _adyenPaymentResponse;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdyenPaymentAdapter"/> class.
    /// </summary>
    /// <param name="adyenPaymentResponse">The Adyen payment response to be adapted. <see cref="PaymentResponse"/></param>
    public AdyenPaymentAdapter(PaymentResponse adyenPaymentResponse)
    {
        _adyenPaymentResponse = adyenPaymentResponse;
    }

    public string TransactionId => _adyenPaymentResponse.PspReference;
    public decimal Amount => CurrencyConverter.FromMinorUnits(_adyenPaymentResponse.Amount.Value ?? 0 , _adyenPaymentResponse.Amount.Currency);
    public string Currency => _adyenPaymentResponse.Amount.Currency;
    public PaymentStatus Status => ConvertAdyenResultCodeToPaymentStatus(_adyenPaymentResponse.ResultCode);
    public string ResultType => _adyenPaymentResponse.ResultCode != null ? _adyenPaymentResponse.ResultCode.Value.GetDescription() : "Unknown";

    private PaymentStatus ConvertAdyenResultCodeToPaymentStatus(PaymentResponse.ResultCodeEnum? resultCode)
    {
        switch (resultCode)
        {
            case PaymentResponse.ResultCodeEnum.Authorised:
            case PaymentResponse.ResultCodeEnum.Pending:
            case PaymentResponse.ResultCodeEnum.Received:
            case PaymentResponse.ResultCodeEnum.Success:
                return PaymentStatus.Paid;
            case PaymentResponse.ResultCodeEnum.Cancelled:
            case PaymentResponse.ResultCodeEnum.Error:
            case PaymentResponse.ResultCodeEnum.Refused:
                return PaymentStatus.Failed;
            default:
                throw new NotSupportedException("Unsupported ResultCode");
        }
    }
}