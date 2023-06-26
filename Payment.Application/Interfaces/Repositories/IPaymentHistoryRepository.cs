using Payment.Domain.DomainModels;
using Payment.Domain.DomainModels.Payment;

namespace Payment.Application.Interfaces.Repositories;

public interface IPaymentHistoryRepository: IBaseRepository<PaymentHistoryEntity, int>  {}