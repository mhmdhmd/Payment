using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Payment.Application.Interfaces.Repositories;
using Payment.Domain.DomainModels.Payment;
using Payment.Infrastructure.Persistence.DbContext;

namespace Payment.Infrastructure.Persistence.Repositories;

public class PaymentHistoryRepository : GenericRepository<PaymentHistoryEntity, int>, IPaymentHistoryRepository
{
    public PaymentHistoryRepository(IMyDbContext dbContext) : base(dbContext){}
}