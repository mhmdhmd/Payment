using Payment.Domain.DomainModels.Payment;

namespace Payment.Application.Interfaces.Repositories;

public interface IPaymentRepository : IBaseRepository<PaymentHistoryEntity,int>
{
}