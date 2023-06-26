using Microsoft.EntityFrameworkCore;
using Payment.Domain.DomainModels.Order;
using Payment.Domain.DomainModels.Payment;
using Payment.Domain.DomainModels.Shopper;

namespace Payment.Infrastructure.Persistence.DbContext;

public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
    {
    }

    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailEntity> OrderDetails { get; set; }
    public DbSet<PaymentHistoryEntity> PaymentHistories { get; set; }
    public DbSet<ShopperEntity> Shoppers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<OrderDetailEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(od => od.OrderEntity)
                .WithMany(o => o.OrderDetails);
        });
        
        modelBuilder.Entity<PaymentHistoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        
        modelBuilder.Entity<ShopperEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        
        modelBuilder.Seed();
    }
}