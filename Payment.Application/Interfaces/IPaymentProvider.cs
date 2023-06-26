using Payment.Application.ViewModels;
using Payment.Domain.DomainModels.Payment;

namespace Payment.Application.Interfaces;

public interface IPaymentProvider
{
    string Name { get; }
    Task<IPaymentResponse> PayAsync(PaymentInfo payment);
}