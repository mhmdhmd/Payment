using Microsoft.EntityFrameworkCore;
using Payment.Domain.DomainModels.Order;
using Payment.Domain.DomainModels.Payment;
using Payment.Domain.DomainModels.Shopper;

namespace Payment.Infrastructure.Persistence.DbContext;

/// <summary>
/// Extension methods for the <see cref="ModelBuilder"/> class to seed the database with initial data.
/// </summary>
public static class ModelBuilderExtension
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> instance.</param>
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // Seed data for ShopperEntity
        modelBuilder.Entity<ShopperEntity>().HasData(
            new ShopperEntity { Id = 1, FName = "John", LName = "Doe", Address = "123 Main St" },
            new ShopperEntity { Id = 2, FName = "Jane", LName = "Smith", Address = "456 Elm St" }
        );

        // Seed data for OrderEntity
        modelBuilder.Entity<OrderEntity>().HasData(
            new OrderEntity { Id = 1, OrderDate = DateTime.Now, TotalPrice = 22.70m, ShopperEntityId = 1 },
            new OrderEntity { Id = 2, OrderDate = DateTime.Now, TotalPrice = 25.99m, ShopperEntityId = 2 }
        );

        // Seed data for OrderDetailEntity
        modelBuilder.Entity<OrderDetailEntity>().HasData(
            new OrderDetailEntity { Id = 1, OrderEntityId = 1, ProductName = "Product 1", Price = 10.50m },
            new OrderDetailEntity { Id = 2, OrderEntityId = 1, ProductName = "Product 2", Price = 12.20m },
            new OrderDetailEntity { Id = 3, OrderEntityId = 2, ProductName = "Product 3", Price = 25.99m }
        );

        // Seed data for PaymentHistoryEntity
        modelBuilder.Entity<PaymentHistoryEntity>().HasData(
            new PaymentHistoryEntity { Id = 1, OrderEntityId = 1, PayDate = DateTime.Now, Status = 0, TransactionId = "123456789" },
            new PaymentHistoryEntity { Id = 2, OrderEntityId = 2, PayDate = DateTime.Now, Status = 1, TransactionId = "987654321" }
        );
    }
}