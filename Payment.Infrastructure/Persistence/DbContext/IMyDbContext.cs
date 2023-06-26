using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Payment.Domain.DomainModels.Order;
using Payment.Domain.DomainModels.Payment;
using Payment.Domain.DomainModels.Shopper;

namespace Payment.Infrastructure.Persistence.DbContext;

public interface IMyDbContext
{
    DbSet<OrderEntity> Orders { get; set; }
    DbSet<OrderDetailEntity> OrderDetails { get; set; }
    DbSet<PaymentHistoryEntity> PaymentHistories { get; set; }
    DbSet<ShopperEntity> Shoppers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<T> GetDbSet<T>() where T : class;
    void Dispose();
}